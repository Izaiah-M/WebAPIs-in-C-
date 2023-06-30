using AspNetCoreRateLimit;
using Marvin.Cache.Headers;
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
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
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

        // Don't forget to first download the necessary package before you do this one
        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        {

            // This is so the API sends caching info in the headers back the client
            // Such that in the event that the DB gets updated whille the client is using cached data
            // The app will know when to make a new call to the DB so that the client is not getting stale data.

            services.AddResponseCaching();
            services.AddHttpCacheHeaders(expOpt =>
            {
                expOpt.MaxAge = 65;
                expOpt.CacheLocation = CacheLocation.Private;
            },

            (validationOpt) =>
            {
                validationOpt.MustRevalidate = true;
            }

            );
        }

        public static void ConfigureRateLimitting(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule> { 
                
                // So what this means is that to all the endpoints
                // All endpoints are limited to one(1) call every 10seconds
                new RateLimitRule {
                    Endpoint = "*",
                    Limit = 1,
                    Period = "10s"
                
                } 
            };

            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules;
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
    }
}
