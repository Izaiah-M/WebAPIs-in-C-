using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Project1.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double Rating { get; set; }

        [ForeignKey(nameof(Country))]
        // Id comes first
        public int CountryId { get; set; }

        public Country? Country { get; set; }

    }
}
