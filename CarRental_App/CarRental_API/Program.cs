using CarRental_Application.Interfaces;
using CarRental_Application.Repositories;
using CarRental_Application.Services;
using CarRental_Infrastructure.Helpers;
using CarRental_Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Register Configuration
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add DB service
builder.Services.AddDbContext<CarRentalDBContext>(opt => 
    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CarRental_Infrastructure")));

builder.Services.AddScoped<IClientServices, ClientServices>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IReservationServices, ReservationServices>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IVehicleTypeServices, VehicleTypeServices>();
builder.Services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
builder.Services.AddScoped<IVehicleManufacturerServices, VehicleManufacturerServices>();
builder.Services.AddScoped<IVehicleManufacturerRepository, VehicleManufacturerRepository>();
builder.Services.AddScoped<IVehicleServices, VehicleServices>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
