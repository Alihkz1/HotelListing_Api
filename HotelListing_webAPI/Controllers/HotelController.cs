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
    

        public async Task<object> GetHotels()
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll();
                var results = _mapper.Map<IList<HotelDTO>>(hotels);
                return Ok(results);
            }
            catch(Exception ex)
            {
                _logger.LogError($"something went wrong at {nameof(GetHotels)}", ex);
                return StatusCode(500, "Internal Server Error ! pls try again later");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Countries.Get(q => q.Id == id, new List<string> { "Contry" });
                var result = _mapper.Map<HotelDTO>(hotel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"something went wrong at {nameof(GetHotel)}", ex);
                return StatusCode(500, "Internal Server Error ; pls try again later");
            }
        }

    }
}