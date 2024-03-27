using HotelListing.DTO.Request;

namespace HotelListing.DTO.Response
{
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public virtual IList<HotelDTO> Hotels { get; set; }
    }

}
