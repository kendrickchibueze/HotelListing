using AutoMapper;
using HotelListing.DTO.Request;
using HotelListing.DTO.Response;

namespace HotelListing.Data.Configurations
{
    public class MapperInitializer : Profile
    {

        public MapperInitializer()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<UpdateCountryDTO, Country>()
           .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore mapping for the Id property
           .ForMember(dest => dest.Hotels, opt => opt.MapFrom(src => src.Hotels));
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
        }
    }
}
