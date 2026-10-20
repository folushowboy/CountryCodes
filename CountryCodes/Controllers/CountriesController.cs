using CountryCodes.Services;
using CountryCodes.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CountryCodes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        /// <summary>
        /// Gets the details of a country based on its calling code.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns>The country and its operator details</returns>
        [HttpGet("GetCountryDetails")]
        public async Task<IActionResult> GetCountries([FromQuery] string? phoneNumber)
        {
            var countriesResult = await _countryService.GetCountryDetailsAsync(phoneNumber);

            if (!countriesResult.Success)
            {
                return StatusCode(countriesResult.StatusCode, new
                {
                    message = countriesResult.ErrorMessage
                });
            }
            return Ok(countriesResult.Data);
        }
    }
}
