using Serilog; // For all our logging needs
using Serilog.Core;
using System.Linq.Expressions;

var builder = WebApplication.CreateBuilder(args);

// Configuring our logger with serilogger.
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Using serilogger for the requests
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// To test the logging

try
{
Log.Logger.Information("Application is running on Port 60000");
app.Run();

}catch (Exception ex)
{
    Log.Logger.Fatal(ex, "Application failed to start");
}

