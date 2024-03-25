using AutoMapper;
using HotelListing.Data;
using HotelListing.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Hotel> _hotelRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelService> _logger;
        public HotelService(ILogger<HotelService> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hotelRepo = _unitOfWork.GetRepository<Hotel>();
            _logger = logger;
        }
        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            var hotels = await _hotelRepo.GetByAsync(include: query => query.Include(x => x.Country));
            if (hotels == null) _logger.LogError("No country found");
            return hotels.ToList();
        }
        public async Task<Hotel> GetHotel(int id)
        {
            var hotel = await _hotelRepo.GetSingleByAsync(predicate: x => x.Id == id, include: query => query.Include(x => x.Country));
            if (hotel == null) _logger.LogError($"hotel with id {id} does not exist");
            return hotel;
        }
        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            var newHotel = await _hotelRepo.AddAsync(hotel);
            if (newHotel == null) _logger.LogError("Unable to Add a Hotel");
            return newHotel;
        }
        public async Task<string> DeleteHotel(int id)
        {
            var hotel = await _hotelRepo.GetSingleByAsync(predicate: x => x.Id == id);
            if (hotel == null)
                _logger.LogError($"hotel with the ${id} does not exist");
            await _hotelRepo.DeleteAsync(x => x.Id == id);
            return $"successfully deleted hotel with Id ${id}";
        }
        public async Task UpdateHotel(int id, Hotel hotel)
        {
            var existingHotel = await _hotelRepo.GetSingleByAsync(predicate: x => x.Id == id);
            if (existingHotel == null)
                _logger.LogError($"hotel with the ${id} does not exit");
            existingHotel = _mapper.Map<Hotel>(hotel);
            existingHotel.Id = id;
            await _hotelRepo.UpdateAsync(existingHotel);
        }
    }
}
