using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Project1.DTOs.Hotel
{
    public class HotelDTO
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(50)]
        public string? Address { get; set; }
        public double Rating { get; set; }

        // Id comes first
        [Required]
        public int CountryId { get; set; }

      
    }
}
