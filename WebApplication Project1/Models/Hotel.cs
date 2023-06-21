using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Project1.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }

        [ForeignKey(nameof(Country))]

        public Country Country { get; set; }

        public int CountryId { get; set; }
    }
}
