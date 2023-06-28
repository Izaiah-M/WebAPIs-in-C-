using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_Project1.Configurations.EntityConfigurations;

namespace WebApplication_Project1.Models
{
    // For security we are going to extend IdentityDbContext
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        // Define the context contructor using "cotr"
        public DatabaseContext(DbContextOptions <DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // adding our role configuration from our configurations class
            builder.ApplyConfiguration(new RoleConfig());
        }

        // Registering the tables that shall be set in the DB...via serialization

        // The datatype is going to be 'Country'...ie <Country>...ie what the Model is named
        public DbSet<Country> Countries { get; set; } // The name that is given here "Countries" is the one that the database will use...not the one defined in the models

        // The datatype is going to be 'Hotel'...ie <Hotel>...ie what the Model is named
        public DbSet<Hotel> Hotels { get; set; } // The name that is given here "Hotels" is the one that the database will use...not the one defined in the models
    }
}
