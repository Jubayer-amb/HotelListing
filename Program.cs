using HotelListing.Authorization;
using HotelListing.Data;
using HotelListing.Helper;
using HotelListing.IRepository;
using HotelListing.Repository;
using HotelListing.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.File(
        path: "..\\logs\\log-.txt",
        outputTemplate: "{Timestamp:yyyy/MM/dd HH:mm:ss} [{Level:u3}] {Message:lj} {NewLine}{Exception}{NewLine}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Warning
        ).WriteTo.Console();
});

var services = builder.Services;

// Add services to the container.
{
    services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection"))
    );

    services.Configure<AppSettings>(builder.Configuration.GetSection(AppSettings.Encriptions));

    services.AddCors(o =>
    {
        o.AddPolicy("AllowAll", b =>
            b.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
    });

    services.AddAutoMapper(typeof(Program));


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListing", Version = "v1" });
    });

    services.AddControllers().AddNewtonsoftJson(op =>
        op.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore);


    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();

}

var app = builder.Build();
{

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListing v1"));
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();

    app.Run();
}
