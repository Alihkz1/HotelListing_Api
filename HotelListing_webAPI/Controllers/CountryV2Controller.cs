using System.Threading.Tasks;
using HotelListing_webAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing_webAPI.Controllers
{
    [ApiVersion("2.0" , Deprecated = true)] // Deprecated : says it is not probably the preferred version of this route's apis
    [Route("api/{v:apiversion}/country")]
    [ApiController]
    public class CountryVersionTwoController : ControllerBase
    {
        private DatabaseContext _context;

            public CountryVersionTwoController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(_context.Countries);
        }
    }
}
