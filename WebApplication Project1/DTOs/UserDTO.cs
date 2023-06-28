using System.ComponentModel.DataAnnotations;

namespace WebApplication_Project1.DTOs
{
    // Don't forget to add your DTO to the mapper config which is in the configurations folder

    public class LoginDTO
    {
        [Required]
        [StringLength(100)]
        //[DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        [StringLength(15)]
        public required string Password { get; set; }
    }

    public class UserDTO : LoginDTO
    {
        [Required]
        [StringLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public required string LastName { get; set; }

        public ICollection<string>? Roles { get; set; }

    }
}
