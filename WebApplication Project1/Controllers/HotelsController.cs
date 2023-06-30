using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using WebApplication_Project1.DTOs;
using WebApplication_Project1.DTOs.Hotel;
using WebApplication_Project1.IRepository;
using WebApplication_Project1.Models;
using WebApplication_Project1.Repository;

namespace WebApplication_Project1.Controllers
{
    
    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelsController> _logger;

        public HotelsController(IUnitOfWork unitOfWork, ILogger<HotelsController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
             _logger = logger;
            _mapper = mapper;
        }

     
        [Authorize(Roles = "Super Administrator")]
        [HttpPost]
        [Route("create-hotel")]
        public async Task<IActionResult> CreateHotel([FromBody] HotelDTO hotelDTO)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Missing Fields Address: {hotelDTO.Address} Name: {hotelDTO.Name} Rating: {hotelDTO.Address}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = _mapper.Map<Hotel>(hotelDTO);

                await _unitOfWork.HotelRepository.Insert(hotel);

                await _unitOfWork.Save();

                return Created("Hotel successfully created", hotel);

            }
            catch (Exception ex)
            {

                _logger.LogInformation($"Something went wrong, {nameof(CreateHotel)} ", ex);

                return Problem($"Something went wrong, {nameof(CreateHotel)}", statusCode: 500);
            }

        }

        [Authorize(Roles = "Super Administrator")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {

                return BadRequest(ModelState);
            }

            try
            {

                var hotel = await _unitOfWork.HotelRepository.Get(h => h.Id == id);

                if (hotel == null)
                {
                    return BadRequest("Hotel not Found");
                }

                _mapper.Map(hotelDTO, hotel);
                _unitOfWork.HotelRepository.Update(hotel);
                await _unitOfWork.Save();

                return Created("Updated Hotel", hotel);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Something went wrong, {nameof(UpdateHotel)} ", ex);

                return Problem("Internal server Error, please try again later", statusCode: 500);
            }
        }
    }
}
