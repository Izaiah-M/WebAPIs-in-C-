using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication_Project1.Models
{
    public class DatabaseContext : DbContext
    {
        // Define the context contructor using "contr"
        public DatabaseContext(DbContextOptions <DatabaseContext> options) : base(options)
        {
            
        }

        // Registering the tables that shall be set in the DB...via serialization
        
        // The datatype is going to be 'Country'...ie <Country>...ie what the Model is named
        public DbSet<Country> Countries { get; set; } // The name that is given here "Countries" is the one that the database will use...not the one defined in the models

        // The datatype is going to be 'Hotel'...ie <Hotel>...ie what the Model is named
        public DbSet<Hotel> Hotels { get; set; } // The name that is given here "Hotels" is the one that the database will use...not the one defined in the models
    }
}
