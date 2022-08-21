using HotelLIsting.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.File(
        path: "..\\logs\\log-.txt",
        outputTemplate: "{Timestamp:yyyy/MM/dd HH:mm:ss} [{Level:u3}] {Message:lj} {NewLine}{Exception}{NewLine}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information
        ).WriteTo.Console();
});

// Add services to the container.

services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection"))
);

services.AddCors(o =>
{
    o.AddPolicy("AllowAll", b =>
        b.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListing", Version = "v1" });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListing v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


