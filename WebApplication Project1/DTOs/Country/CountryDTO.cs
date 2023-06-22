using System.ComponentModel.DataAnnotations;

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
}
