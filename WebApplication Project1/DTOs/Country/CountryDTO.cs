using System.ComponentModel.DataAnnotations;
using WebApplication_Project1.DTOs.Hotel;

namespace WebApplication_Project1.DTOs.Country
{
    public class CountryDTO
    {
        // This is what I want the user to be able to see and provide when creating a Country

        // Or even viewing it, So its like abstracting what I dont want users of my endpoints to see

        // Don't forget to update the function(Post, get) whichever in the controller to use this DTO
        [Required]
        [StringLength(100)]

        public string? Name { get; set; }

        [Required]
        [StringLength(10)]
        public string? ShortName { get; set; }
    }

    // Can define a DTO for every method
    // And use inheritence to reduce workLoad
    public class GetCountryDTO : CountryDTO
    {
        public int Id { get; set; }

        public  IList<HotelDTO>? Hotels { get; set; }

    }
}
