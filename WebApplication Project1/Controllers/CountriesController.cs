using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marvin.Cache.Headers;
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
        // Adding Caching abilities to our application
        // This can be removed because in our Program.cs and servicExtensions.cs we have made caching global.
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)] // Overriding the global caching settings that have been set up if there is need to
        [HttpCacheValidation(MustRevalidate = false)] // Overriding cache setting for validating when the data in the Database changes.
        public async Task<ActionResult> GetCountries()
        {

            // Removed the try catch to test our Global exception Handler

            //throw new Exception();

            var countries = await _unitOfWork.CountryRepository.GetAllAsync();

            var results = mapper.Map<List<GetCountryDTO>>(countries);

            return Ok(results);



        }

        // GET: api/countries/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetCountry(int id)
        {

            var country = await _unitOfWork.CountryRepository.Get(c => c.Id == id, new List<string> { "Hotels" });

            var result = mapper.Map<GetCountryDTO>(country);

            return Ok(result);


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


            await _context.SaveChangesAsync();

            if (!CountryExists(id))
            {
                return NotFound();
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
