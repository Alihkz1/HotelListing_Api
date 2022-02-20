using AutoMapper;
using HotelListing_webAPI.Data;
using HotelListing_webAPI.Models;

namespace HotelListing_webAPI.Configrations
{
    public class MapperInitillizer : Profile
    {
        public MapperInitillizer()
        {
            CreateMap<Country , CountryDTO>().ReverseMap();
            CreateMap<Country , CreateCountryDTO>().ReverseMap();
            CreateMap<Country , HotelDTO>().ReverseMap();
            CreateMap<Country , CreateHotelDTO>().ReverseMap();
        }
    }
}
