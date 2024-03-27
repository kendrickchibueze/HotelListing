using System.ComponentModel.DataAnnotations;
using HotelListing.DTO.Request;

namespace HotelListing.DTO.Response
{
    public class HotelDTO : CreateHotelDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
    }
}
