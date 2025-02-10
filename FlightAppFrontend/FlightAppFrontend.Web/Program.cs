using Blazored.LocalStorage;
using FlightAppFrontend.Shared.Auth;
using FlightAppFrontend.Shared.Services;
using FlightAppFrontend.Web.Components;
using FlightAppFrontend.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddHttpClient("DirigibleApi", client =>
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("DirigibleApi")!));


builder.Services.AddHttpClient("WeatherApp", client =>
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("WeatherApp")!));

builder.Services.AddHttpClient("OpenCageBase", client =>
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("OpenCageBase")!));

builder.Services.AddHttpClient("AircraftPhotos", client =>
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("AircraftPhotos")!));


builder.Services.AddHttpClient("MapData", client =>
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("MapData")!));




builder.Services.AddScoped<IJsInteropService, JsInteropService>();
builder.Services.AddScoped<TokenStateService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

builder.Services.AddBlazoredLocalStorage();


// Add device-specific services used by the FlightAppFrontend.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
//app.UseAuthentication();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(FlightAppFrontend.Shared._Imports).Assembly);

app.Run();
