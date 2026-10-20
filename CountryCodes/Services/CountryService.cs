using CountryCodes.Data;
using CountryCodes.Dtos;
using CountryCodes.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CountryCodes.Services
{
    public class CountryService : ICountryService
    {
        private readonly CountryCodesDbContext _context;

        public CountryService(CountryCodesDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<CountryLookupResponseDto>> GetCountryDetailsAsync(string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return new ServiceResult<CountryLookupResponseDto>
                {
                    Success = false,
                    ErrorMessage = "Phone number is required.",
                    StatusCode = 400
                };
            }

            string number = phoneNumber.Trim();

            string normalizedNumber = number.StartsWith("+")
                ? number[1..]
                : number;

            if (normalizedNumber.Length == 0 ||
                !normalizedNumber.All(char.IsAsciiDigit))
            {
                return new ServiceResult<CountryLookupResponseDto>
                {
                    Success = false,
                    ErrorMessage = "Enter a valid phone number.",
                    StatusCode = 400
                };
            }

            var countries = await _context.Countries
                .Include(c => c.CountryDetails)
                .AsNoTracking()
                .ToListAsync();

            var country = countries
                .Where(c => normalizedNumber.StartsWith(
                    c.CountryCode.ToString()))
                .OrderByDescending(c =>
                    c.CountryCode.ToString().Length)
                .FirstOrDefault();

            if (country == null)
            {
                return new ServiceResult<CountryLookupResponseDto>
                {
                    Success = false,
                    ErrorMessage = "No country found for this phone code.",
                    StatusCode = 404
                };
            }

            var response = new CountryLookupResponseDto
            {
                Number = number,
                Country = new CountryResponseDto
                {
                    CountryCode = country.CountryCode.ToString(),
                    Name = country.Name,
                    CountryIso = country.CountryIso,
                    CountryDetails = country.CountryDetails
                        .Select(d => new CountryDetailResponseDto
                        {
                            Operator = d.Operator,
                            OperatorCode = d.OperatorCode
                        })
                        .ToList()
                }
            };

            return new ServiceResult<CountryLookupResponseDto>
            {
                Success = true,
                Data = response,
                StatusCode = 200
            };
        }
    }
}
