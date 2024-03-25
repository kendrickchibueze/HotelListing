using HotelListing.Data;

namespace HotelListing.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<Hotel>> GetHotels();
        Task<Hotel> GetHotel(int id);
        Task<Hotel> CreateHotel(Hotel hotel);
        Task UpdateHotel(int id, Hotel hotel);
        Task<string> DeleteHotel(int id);
    }
}

