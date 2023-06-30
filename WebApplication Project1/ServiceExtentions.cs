using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using WebApplication_Project1.DTOs;

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

        // Setting up how we want our exception handling to occur
        public static void ConfigureExceptionHandler(this IApplicationBuilder app) {
            app.UseExceptionHandler( error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        Log.Error($"Something went wrong in the {contextFeature.Error}");

                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal server error, please try again later"
                        }.ToString());
                    }
                });
            });
        }
    }
}
