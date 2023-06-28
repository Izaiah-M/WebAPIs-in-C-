using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Project1.Models
{
    public class ApiRoles : IdentityRole
    {
        public string Description { get; set; } = null!;

        public string AccessLevel { get; set; } = null!;
    }
}
