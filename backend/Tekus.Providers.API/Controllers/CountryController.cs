#region Usings
using Microsoft.AspNetCore.Mvc;
using Tekus.Providers.Application.DTOs.Country;
using Tekus.Providers.Application.Enum;
using Tekus.Providers.Application.Extensions;
using Tekus.Providers.Application.Interfaces.Country;
#endregion

namespace Tekus.Providers.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    #region Instances
    private readonly ICountryService _countryService; 
    #endregion

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCountriesAsync()
    {
        IEnumerable<CountryDTO> countries = await _countryService.GetAllCountriesAsync();
        return Ok(countries.AsResponseDTO(ResponseCodeEnum.OK));
    }

    [HttpGet("code")]
    public async Task<IActionResult> GetCountryByCodeAsync(string code)
    {
        CountryDTO? country = await _countryService.GetCountryByCodeAsync(code);
        return Ok(country.AsResponseDTO(ResponseCodeEnum.OK));
    }
}
