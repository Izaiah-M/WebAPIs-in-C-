using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication_Project1.Models;

namespace WebApplication_Project1.Configurations.EntityConfigurations
{
    public class RoleConfig : IEntityTypeConfiguration<ApiRoles>
    {
        public void Configure(EntityTypeBuilder<ApiRoles> builder)
        {
            builder.HasData(
                new ApiRoles
                {
                    Name = "Super Administrator",
                    NormalizedName = "SUPER ADMINISTRATOR",
                    Description = "Super Admin role",
                    AccessLevel = "/[\"admin dashboard/\", \"hotel dashboard\", \"user dashboard\"]"
                    // there is nothing more you can add here because it is of type Identity Role....if you wanted a personalized thing
                    // You can create your own class for role and add the necessary field
                    // Try it out with the "Country" and "Hotel" Object.
                    // Or just create your own role thing and extend the identityrole thing and then add your own field
                },
                new ApiRoles
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Description = "Admin role",
                    AccessLevel = "/[\"admin dashboard/\"]"
                },
                new ApiRoles
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Description = "customer role",
                    AccessLevel = "/[\"user dashboard/\"]"
                }
                );
        }
    }
}
