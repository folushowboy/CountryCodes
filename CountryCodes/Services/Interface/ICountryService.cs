using CountryCodes.Dtos;

namespace CountryCodes.Services.Interface
{
    public interface ICountryService
    {
        Task<ServiceResult<CountryLookupResponseDto>> GetCountryDetailsAsync(string? phoneNumber);
    }
}
