using AutoMapper;
using HotelListing.Helper;
using HotelListing.Authorization;
using HotelListing.Data;
using HotelListing.Data.Entities;
using HotelListing.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Services;
public interface IUserService
{
    Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO model);
    Task<AuthenticateResponseDTO> Register(CreateUserDTO model);
    Task<IEnumerable<UserDTO>> GetAll();
    Task<User> GetById(Guid id);
    Task Update(Guid id, UpdateUserDTO model);
    Task Delete(Guid id);
    Task<bool> IsUniqueUser(string email);
}
public class UserService : IUserService
{
    private readonly DatabaseContext _context;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public UserService(IMapper mapper, DatabaseContext context, IJwtUtils jwtUtils)
    {
        _mapper = mapper;
        _context = context;
        _jwtUtils = jwtUtils;
    }
    public async Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO model)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == model.Email);
        if (user == null || !EncryptionHelper.VerifyHash(model.Password, user.HashedPassword))
            throw new AppException("Invalid email or password");

        var token = _jwtUtils.GenerateToken(user);

        var response = _mapper.Map<AuthenticateResponseDTO>(user);

        response.Token = token;

        return response;
    }

    public async Task<AuthenticateResponseDTO> Register(CreateUserDTO model)
    {
        if (await IsUniqueUser(model.Email))
            throw new AppException($"{model.Email} is already taken");

        var user = _mapper.Map<User>(model);
        user.HashedPassword = EncryptionHelper.GenerateHash(model.Password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var response = _mapper.Map<AuthenticateResponseDTO>(user);

        var token = _jwtUtils.GenerateToken(user);

        response.Token = token;

        return response;

    }

    public async Task<User> GetById(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new KeyNotFoundException(message: "User not found");
        return user;
    }

    public async Task<IEnumerable<UserDTO>> GetAll()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public async Task Update(Guid id, UpdateUserDTO model)
    {
        var user = await GetById(id);

        if (user.Email != model.Email && await IsUniqueUser(model.Email))
            throw new AppException("Invalid Email or Email has already taken");

        if (!string.IsNullOrEmpty(model.Password))
            user.HashedPassword = EncryptionHelper.GenerateHash(model.Password);

        _mapper.Map(model, user);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        var user = await GetById(id);

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsUniqueUser(string email)
    {
        return await _context.Users.AnyAsync(x => x.Email == email);
    }
}

