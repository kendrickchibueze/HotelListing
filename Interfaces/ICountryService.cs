using HotelListing.Data;

namespace HotelListing.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<Country> GetCountry(int id);
        Task<Country> CreateCountry(Country country);
        Task<string> DeleteCountry(int id);
    }
}
