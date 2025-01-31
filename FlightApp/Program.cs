using FlightApp.Repository;
using FlightApp.Database;
using FlightApp.Models;
using Microsoft.AspNetCore.Identity;
using FlightApp.Service;
using FlightApp.Mapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FlightAppDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<FlightAppDbContext>();

builder.Services.AddDbContext<FlightAppDbContext>();

builder.Services.AddScoped<IFlightApiRepository, FlightApiRepository>();
builder.Services.AddScoped<IFlightApiService, FlightApiService>();
builder.Services.AddScoped<IFlightsRepository, FlightsRepository>();
builder.Services.AddScoped<INotesRepository, NotesRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IFlightsService, FlightsService>();
builder.Services.AddScoped<INotesService, NotesService>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
