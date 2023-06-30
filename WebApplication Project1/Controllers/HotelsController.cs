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

            // We are not using a try-catch block because we have a global error handler configured in our "ServiceExtensions class"

            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Missing Fields Address: {hotelDTO.Address} Name: {hotelDTO.Name} Rating: {hotelDTO.Address}");
                return BadRequest(ModelState);
            }

            
                var hotel = _mapper.Map<Hotel>(hotelDTO);

                await _unitOfWork.HotelRepository.Insert(hotel);

                await _unitOfWork.Save();

                return Created("Hotel successfully created", hotel);


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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id < 1)
            {

                return BadRequest("Hotel does not exist");
            }

            try
            {

                var hotel = await _unitOfWork.HotelRepository.Get(h => h.Id == id);

                if (hotel == null)
                {
                    return BadRequest("Hotel not Found");
                }

                await _unitOfWork.HotelRepository.Delete(id);

                await _unitOfWork.Save();

                return Ok($"Successfully deleted {hotel.Name}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Something went wrong, {nameof(DeleteHotel)} ", ex);

                return Problem("Internal server Error, please try again later", statusCode: 500);
            }
        }
    }
}
