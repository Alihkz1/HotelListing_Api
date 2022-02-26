using System;
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
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;

        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetHotels()
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll();
                //  var results = _mapper.Map<IList<HotelDTO>>(hotels);
                return Ok(hotels);
            }

            catch (Exception ex)
            {
                _logger.LogError($"something went wrong at {nameof(GetHotels)}", ex);
                return StatusCode(500, "Internal Server Error ! please try again later");
            }
        }

        [HttpGet("{id:int}", Name = "GetHotel")] // Name : is like an internal nickname !!
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(q => q.Id == id);
                var result = _mapper.Map<HotelDTO>(hotel);
                return Ok(hotel);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"something went wrong at {nameof(GetHotel)}");
                return StatusCode(500, "Internal Server Error ; please try again later");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(CreateHotel)}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = _mapper.Map<Hotel>(hotelDTO);
                await _unitOfWork.Hotels.Insert(hotel);
                await _unitOfWork.save();
                return CreatedAtRoute("GetHotel", new { id = hotel.Id }, hotel);
            }
                
            catch (Exception ex)
            {
                _logger.LogError(ex, $"something went wrong at {nameof(CreateHotel)}");
                return StatusCode(500, "Internal Server Error ! please try again later");

            }

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel( int id , [FromBody] UpdateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(UpdateHotel)}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = await _unitOfWork.Hotels.Get(q => q.Id == id);
                if(hotel == null)
                {
                    _logger.LogError($"Invalid Update attempt in {nameof(UpdateHotel)}");
                    return BadRequest("Submitted Data Is Invalid ! ");
                }
                _mapper.Map(hotelDTO, hotel);
                _unitOfWork.Hotels.Update(hotel);
                await _unitOfWork.save();
                return NoContent();
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, $"something went wrong at {nameof(UpdateHotel)}");
                return StatusCode(500, "Internal Server Error ! please try again later");
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHotel(int id, [FromBody] UpdateHotelDTO hotelDTO)
        {
            if ( id < 1)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(DeleteHotel)}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = await _unitOfWork.Hotels.Get(q => q.Id == id);
                if (hotel == null)
                {
                    _logger.LogError($"Invalid Update attempt in {nameof(DeleteHotel)}");
                    return BadRequest("Submitted Data Is Invalid ! ");
                }
                await _unitOfWork.Hotels.Delete(id);
                await _unitOfWork.save();
                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"something went wrong at {nameof(DeleteHotel)}");
                return StatusCode(500, "Internal Server Error ! please try again later");
            }
        }

    }

}
