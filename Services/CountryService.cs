using AutoMapper;
using HotelListing.Data;
using HotelListing.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Services
{
    public class CountryService:ICountryService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Country> _countryRepo;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _countryRepo = _unitOfWork.GetRepository<Country>();
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            var countries = await _countryRepo.GetByAsync(include: query => query.Include(x => x.Hotels));
            if (countries == null) throw new InvalidOperationException("No country found");
            return countries.ToList();
        }
        public async Task<Country> GetCountry(int id)
        {
            var country = await _countryRepo.GetSingleByAsync(x => x.Id == id, include: query => query.Include(x => x.Hotels));
            if (country == null) throw new InvalidOperationException($"country with id {id} does not exist");
            return country;
        }

        public async Task<Country> CreateCountry(Country country)
        {
            var newCountry = await _countryRepo.AddAsync(country);
            if (newCountry == null) throw new InvalidOperationException("Unable to Add a Country");
            return newCountry;
            
        }

        public async Task<string> DeleteCountry(int id)
        {
            var country = await _countryRepo.GetSingleByAsync(predicate: x => x.Id == id);
            if (country == null)
                throw new InvalidOperationException($"country with the ${id} does not exist");
            await _countryRepo.DeleteAsync(x => x.Id == id);
            return $"successfully deleted country with Id ${id}";
            
        }
    }
}
