using AutoMapper;
using HotelListing.Authorization;
using HotelListing.Data;
using HotelListing.Data.Entities;
using HotelListing.Data.Enums;
using HotelListing.Data.Models;
using HotelListing.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;
    private IMapper _mapper;
    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<ActionResult<AuthenticateResponseDTO>> Login(AuthenticateRequestDTO model)
    {
        var response = await _userService.Authenticate(model);
        return Ok(response);
    }


    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Register(CreateUserDTO model)
    {
        var response = await _userService.Register(model);
        return Ok(new { message = "User created successfully", user = response });
    }

    [Authorize(Role.SuperAdmin)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDTO>> GetById(Guid id)
    {
        var currentUser = (User)HttpContext.Items["User"];
        if (id != currentUser.Id && currentUser.Role != Role.Admin && currentUser.Role != Role.SuperAdmin)
            return Unauthorized(new { message = "Unauthorized" });

        var user = await _userService.GetById(id);
        return Ok(user);

    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserDTO model)
    {
        await _userService.Update(id, model);
        return Ok(new { message = "User updated successfully" });
    }

    [Authorize(Role.SuperAdmin, Role.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }

}