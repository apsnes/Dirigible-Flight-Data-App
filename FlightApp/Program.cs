using FlightApp.Repository;
using FlightApp.Database;
using FlightApp.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FlightAppDbContext>();
builder.Services.AddScoped<IFlightApiRepository, FlightApiRepository>();
builder.Services.AddScoped<IFlightApiService, FlightApiService>();


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
