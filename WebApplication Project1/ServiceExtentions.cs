using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApplication_Project1
{
    public static class ServiceExtentions
    {
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt"); // The "Jwt" is from the application.json file
            var key = Environment.GetEnvironmentVariable("KEY"); // this is what you set in your global environment

            services.AddAuthentication(options =>
            {
                // Setting the default authentication scheme to be Jwt.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value, // Again from the appsettings json file regarding the things you set
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // this gets our env variable key that we set and hashes it again
                    };

                });
        }
    }
}
