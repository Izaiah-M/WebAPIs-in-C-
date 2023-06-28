using Microsoft.AspNetCore.Identity;

namespace WebApplication_Project1.Models
{
    public class ApiUser : IdentityUser // Identity User has a bunch of more stuff, you can check them out
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

    }
}
