using AutoMapper;
using HotelListing.Data;
using HotelListing.Interfaces;
using HotelListing.Models;
using HotelListing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryController(IMapper mapper, ICountryService countryService)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet("{id:int}", Name = "GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {        
            var country = await _countryService.GetCountry(id);
            var result = _mapper.Map<CountryDTO>(country);
            return Ok(result);
        }

        [HttpGet( "GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _countryService.GetCountries();
            var results = _mapper.Map<IList<CountryDTO>>(countries);
            return Ok(results);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"Invalid POST attempt in {nameof(CreateCountry)}");

            }
            var country = _mapper.Map<Country>(countryDTO);
            await _countryService.CreateCountry(country);
            return CreatedAtRoute("GetCountry", new { id = country.Id }, country);

        }

        /*  [Authorize]
          [HttpPut("{id:int}")]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
          [ProducesResponseType(StatusCodes.Status204NoContent)]
          [ProducesResponseType(StatusCodes.Status500InternalServerError)]*/
        /*    public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
            {
                if (!ModelState.IsValid || id < 1)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCountry)}");
                    return BadRequest(ModelState);
                }

                var country = await _unitOfWork.Countries.Get(q => q.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCountry)}");
                    return BadRequest("Submitted data is invalid");
                }

                _mapper.Map(countryDTO, country);
                _unitOfWork.Countries.Update(country);
                await _unitOfWork.Save();

                return NoContent();

            }*/

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid DELETE attempt in {nameof(DeleteCountry)}");
            }

            var country = await _countryService.DeleteCountry(id);
            return Ok(country);

        }
    }
}
