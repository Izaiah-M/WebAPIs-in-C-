using AutoMapper;
using WebApplication_Project1.DTOs;
using WebApplication_Project1.DTOs.Country;
using WebApplication_Project1.DTOs.Hotel;
using WebApplication_Project1.Models;

// Don't forget to add this in your program.cs file
namespace WebApplication_Project1.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Country DTOs
            CreateMap<CountryDTO, Country>().ReverseMap();
            CreateMap<GetCountryDTO, Country>().ReverseMap();

            // Hotel DTOs
            CreateMap<HotelDTO, Hotel>().ReverseMap();
            CreateMap<UpdateHotelDTO, Hotel>().ReverseMap();

            // Register and Login DTOs being mapped
            CreateMap<UserDTO, ApiUser>().ReverseMap();

            // Don't need to let the mapper know about the LoginDTO
            // CreateMap<LoginDTO, ApiUser>().ReverseMap();
        }
    }
}
