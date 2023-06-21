using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WebApplication_Project1.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new DatabaseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DatabaseContext>>()))
            {
                // Look for any Countries in the DB.
                // If there are any movies in the database, the seed initializer returns and no movies are added.
                if (context.Countries.Any())
                {
                    return;   // DB has been seeded
                }

                // Otherwise add these countries to the DB
                context.Countries.AddRange(
                    new Country
                    {
                       
                        Name = "Uganda",
                        ShortName = "Ug",
                    },
                    new Country
                    {
                    
                        Name = "Kenya",
                        ShortName = "Ky",
                    }
                  
                );

                // For the Hotels
                if (context.Hotels.Any())
                {
                    return;   // DB has been seeded
                }

                // Otherwise add these countries to the DB
                context.Hotels.AddRange(
                    new Hotel
                    {
                       
                        Name = "HotelMe",
                        Address= "KampalaRd",
                        Rating = 5.5,
                        CountryId = 1,
         
                    },
                    new Hotel
                    {
                       
                        Name = "Shelby Hotel",
                        Address = "KenyaRd",
                        Rating = 4.5,
                        CountryId = 2,
                    }

                );

                context.SaveChanges();
            }
        }
    }
}
