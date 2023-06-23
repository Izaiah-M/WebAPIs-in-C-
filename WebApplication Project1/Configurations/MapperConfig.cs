using AutoMapper;
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
            CreateMap<CountryDTO, Country>().ReverseMap();
            CreateMap<HotelDTO, Hotel>().ReverseMap();
            CreateMap<GetCountryDTO, Country>().ReverseMap();
        }
    }
}
