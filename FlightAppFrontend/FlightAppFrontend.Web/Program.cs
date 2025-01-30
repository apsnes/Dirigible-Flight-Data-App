using FlightAppFrontend.Shared.Services;
using FlightAppFrontend.Web.Components;
using FlightAppFrontend.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("DirigibleApi", client =>
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("DirigibleApi")!));

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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(FlightAppFrontend.Shared._Imports).Assembly);

app.Run();
