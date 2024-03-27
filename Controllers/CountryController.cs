using AutoMapper;
using HotelListing.Data;
using HotelListing.DTO.Request;
using HotelListing.DTO.Response;
using HotelListing.Interfaces;
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

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _countryService.GetCountries();
            var results = _mapper.Map<IList<CountryDTO>>(countries);
            return Ok(results);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost("CreateCountry")]
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

        //[Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest($"Invalid UPDATE attempt in {nameof(UpdateCountry)}");

            }
            var country = _mapper.Map<Country>(countryDTO);
            await _countryService.UpdateCountry(id, country);
            return Ok("Country updated successfully");
        }

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
            return Ok("Country deleted successfully");
        }
    }
}
