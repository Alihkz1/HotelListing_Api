using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing_webAPI.Data;
using HotelListing_webAPI.IRepository;
using HotelListing_webAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelListing_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetPagedList(requestParams);
                var results = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(results);
            }

            catch (Exception ex)
            {
                _logger.LogError($"something went wrong at {nameof(GetCountries)}", ex);
                return StatusCode(500, "Internal Server Error ; please try again later");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id, new List<string> { "Hotels" });
                var result = _mapper.Map<CountryDTO>(country);
                return Ok(result);
            }

            catch (Exception ex)
            {
                _logger.LogError($"something went wrong at {nameof(GetCountry)}", ex);
                return StatusCode(500, "Internal Server Error ; please try again later");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Invalid Update attempt in {nameof(UpdateCountry)}");
                    return BadRequest("Submitted Data Is Invalid ! ");
                }
                _mapper.Map(countryDTO, country);
                _unitOfWork.Countries.Update(country);
                await _unitOfWork.save();
                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"something went wrong at {nameof(UpdateCountry)}");
                return StatusCode(500, "Internal Server Error ! please try again later");
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id, [FromBody] UpdateCountryDTO CountryDTO)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(DeleteCountry)}");
                return BadRequest(ModelState);
            } 

            try
            {
                var country = await _unitOfWork.Hotels.Get(q => q.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Invalid Update attempt in {nameof(DeleteCountry)}");
                    return BadRequest("Submitted Data Is Invalid ! ");
                }
                await _unitOfWork.Countries.Delete(id);
                await _unitOfWork.save();
                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"something went wrong at {nameof(DeleteCountry)}");
                return StatusCode(500, "Internal Server Error ! please try again later");
            }
        }



    }
}
