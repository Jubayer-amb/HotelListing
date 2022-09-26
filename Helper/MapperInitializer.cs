using AutoMapper;
using HotelListing.Data.Entities;
using HotelListing.Data.Models;

namespace HotelListing.Helper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Country, CountryDTO>().ReverseMap();
        CreateMap<Country, CreateCountryDTO>().ReverseMap();
        CreateMap<Hotel, HotelDTO>().ReverseMap();
        CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, CreateUserDTO>().ReverseMap();
        CreateMap<User, AuthenticateRequestDTO>().ReverseMap();
        CreateMap<User, AuthenticateResponseDTO>().ReverseMap();
        CreateMap<UpdateUserDTO, User>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, prop) =>
        {
            if (prop == null) return false;
            if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;
            return true;
        }));

    }
}
