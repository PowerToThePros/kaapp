using kaapp.API.Business;
using kaapp.API.Data;
using kaapp.API.Helpers;
using kaapp.API.Manager;
using Npgsql;
using Scalar.AspNetCore;

// builder ist die bauphase, in der die Anwendung konfiguriert wird

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("Postgres"); // comes from user secrets // view user secrets: dotnet user-secrets list 

builder.Services.AddSingleton(NpgsqlDataSource.Create(connectionString)); // NpgsqlDataSource einmalig registrieren mit dem richtigen ConnectionString

builder.Services.AddScoped<DatabaseContext>(); // add singleton for PostgreSettings

builder.Services.AddControllers();

builder.Services.AddScoped<IKundenData, KundenData>();
builder.Services.AddScoped<IKundenManager, KundenManager>();
builder.Services.AddScoped<IKundenBusiness, KundenBusiness>();

builder.Services.AddScoped<IArtikelData, ArtikelData>();
builder.Services.AddScoped<IArtikelManager, ArtikelManager>();
builder.Services.AddScoped<IArtikelBusiness, ArtikelBusiness>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build(); // konfiguration ist fertig etz wird die app erstellt

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference(); // add scalar 
}

app.UseHttpsRedirection(); // HTTP auf HTTPS umleiten

app.UseAuthorization(); // Berechtigungen prüfen

app.MapControllers(); // Controller Routen mit HTTP Anfragen verbinden

app.Run();
