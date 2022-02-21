using System;
using System.Threading.Tasks;
using HotelListing_webAPI.IRepository;
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
        private readonly ILogger _logger;

        public CountryController(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork= unitOfWork;
            _logger= logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = _unitOfWork.Countries.GetAll(); 
                return Ok(countries);
            }
            catch (Exception ex)
            {
                _logger.LogError($"something went wrong at {nameof(GetCountries)}", ex );
                return StatusCode(500, "Internal Server Error ; pls try again later");
            }
        }

    }
}
