using Microsoft.EntityFrameworkCore;
using Serilog; // For all our logging needs
using Serilog.Core;
using System.Linq.Expressions;
using WebApplication_Project1.Models;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Adding CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4000",
                                              "http://localhost:5173");
                      });
});

// Configuring our logger with serilogger.
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Setting up the database connection
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"))); // Connection string is got from appsettings.json

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

//Also configure swagger for dev mode.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/

// We want swagger to be up regardless of the environment, But if you prefer it only up during dev env, uncoment the stuff above and comment the ones below
app.UseSwagger();
app.UseSwaggerUI();

// Using serilogger for the requests
app.UseSerilogRequestLogging();

// Registering the cors policy
/*
 The call to UseCors must be placed after UseRouting, but before UseAuthorization.
 */
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// To test the logging

try
{
Log.Logger.Information("Application is successfully running");
app.Run();

}catch (Exception ex)
{
    Log.Logger.Fatal(ex, "Application failed to start");
    throw;
}
