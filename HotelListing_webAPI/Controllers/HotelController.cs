﻿using System;
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
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;

        public HotelController (IUnitOfWork unitOfWork , ILogger<HotelController> logger, 
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
            catch(Exception ex)
            {
                _logger.LogError($"something went wrong at {nameof(GetHotels)}", ex);
                return StatusCode(500, "Internal Server Error ! please try again later");
            }
        }   

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(q => q.Id == id);
              //  var result = _mapper.Map<HotelDTO>(hotel);
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"something went wrong at {nameof(GetHotel)}");
                return StatusCode(500, "Internal Server Error ; please try again later");
            }
        }

    }
}
