using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_Project1.DTOs.Country;
using WebApplication_Project1.IRepository;
using WebApplication_Project1.Models;

namespace WebApplication_Project1.Controllers
{
    [Route("api/country")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<CountriesController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CountriesController(DatabaseContext context, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CountriesController> logger)
        {
            _context = context;
            this.mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // GET: api/countries
        [HttpGet]
        public async Task<ActionResult> GetCountries()
        {

            try
            {
                
                var countries = await _unitOfWork.CountryRepository.GetAllAsync();

                var results = mapper.Map<List<GetCountryDTO>>(countries);

                return Ok(results);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                _logger.LogError(ex.Message, $" Something went wrong {nameof(GetCountries)}");
                return StatusCode(500, "Internal Server Error, please try again later.");
            }

        }

        // GET: api/countries/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.CountryRepository.Get(c => c.Id == id, new List<string> { "Hotels" });
                
                var result = mapper.Map<GetCountryDTO>(country);

                return Ok(result);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, $" Something went wrong {nameof(GetCountries)}");
                return StatusCode(500, "Internal Server Error, please try again later.");
            }
        }

        // PUT: api/countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryDTO>> PostCountry(CountryDTO countryDTO)
        {
            // Defining the Country instance to use
            // Another option to take care of this tedious one is to use the Automapper library
            /*var country = new Country
            {
                Name = countryDTO.Name,
                ShortName = countryDTO.ShortName
            };
            */

            // Using the mapper instead of the above method
           var country = mapper.Map<Country>(countryDTO);


            if (_context.Countries == null)
          {
              return Problem("Entity set 'DatabaseContext.Countries'  is null.");
          }
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Country: {country.Name}, shortName: {country.ShortName}, Id: {country.Id}");
            
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country);
        }

        // DELETE: api/countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
