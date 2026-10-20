using CountryCodes.Data;
using CountryCodes.Models;
using CountryCodes.Services;
using CountryCodes.Services.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register controller support.
builder.Services.AddControllers();

// Register Swagger for API documentation and testing.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICountryService, CountryService>();

// Configure the in-memory database.
builder.Services.AddDbContext<CountryCodesDbContext>(options =>
    options.UseInMemoryDatabase("CountryCallingDatabase"));

var app = builder.Build();

// Create the database and initialize its data.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<CountryCodesDbContext>();

    dbContext.Database.EnsureCreated();

    // Seed data only when there are no countries.
    if (!dbContext.Countries.Any())
    {
        dbContext.Countries.AddRange(
            new Country
            {
                Id = 1,
                CountryCode = 234,
                Name = "Nigeria",
                CountryIso = "NG"
            },
            new Country
            {
                Id = 2,
                CountryCode = 233,
                Name = "Ghana",
                CountryIso = "GH"
            },
            new Country
            {
                Id = 3,
                CountryCode = 229,
                Name = "Benin Republic",
                CountryIso = "BN"
            },
            new Country
            {
                Id = 4,
                CountryCode = 225,
                Name = "Côte d'Ivoire",
                CountryIso = "CIV"
            }
        );

        // Save countries before adding their operators.
        dbContext.SaveChanges();

        dbContext.CountryDetails.AddRange(
            new CountryDetail
            {
                Id = 1,
                CountryId = 1,
                Operator = "MTN Nigeria",
                OperatorCode = "MTN NG"
            },
            new CountryDetail
            {
                Id = 2,
                CountryId = 1,
                Operator = "Airtel Nigeria",
                OperatorCode = "ANG"
            },
            new CountryDetail
            {
                Id = 3,
                CountryId = 1,
                Operator = "9 Mobile Nigeria",
                OperatorCode = "ETN"
            },
            new CountryDetail
            {
                Id = 4,
                CountryId = 1,
                Operator = "Globacom Nigeria",
                OperatorCode = "GLO NG"
            },
            new CountryDetail
            {
                Id = 5,
                CountryId = 2,
                Operator = "Vodafone Ghana",
                OperatorCode = "Vodafone GH"
            },
            new CountryDetail
            {
                Id = 6,
                CountryId = 2,
                Operator = "MTN Ghana",
                OperatorCode = "MTN Ghana"
            },
            new CountryDetail
            {
                Id = 7,
                CountryId = 2,
                Operator = "Tigo Ghana",
                OperatorCode = "Tigo Ghana"
            },
            new CountryDetail
            {
                Id = 8,
                CountryId = 3,
                Operator = "MTN Benin",
                OperatorCode = "MTN Benin"
            },
            new CountryDetail
            {
                Id = 9,
                CountryId = 3,
                Operator = "Moov Benin",
                OperatorCode = "Moov Benin"
            },
            new CountryDetail
            {
                Id = 10,
                CountryId = 4,
                Operator = "MTN Côte d'Ivoire",
                OperatorCode = "MTN CIV"
            }
        );

        dbContext.SaveChanges();
    }
}

// Enable Swagger in the development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();