#region Usings
using System.Text.Json;
using Tekus.Providers.Application.DTOs.Country;
using Tekus.Providers.Application.Interfaces.Country;
using Tekus.Providers.Infrastructure.Models.Country;
#endregion

namespace Tekus.Providers.Application.Services;

public class CountryService : ICountryService
{
    #region Instances
    private readonly HttpClient _httpClient;
    private List<CountryDTO>? _cachedCountries;
    #endregion

    public CountryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    #region Get
    public async Task<IEnumerable<CountryDTO>> GetAllCountriesAsync()
    {
        if (_cachedCountries != null)
            return _cachedCountries;

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync("all?fields=cca2,name,flags");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            List<RestCountry>? countries = JsonSerializer.Deserialize<List<RestCountry>>(json);

            _cachedCountries = countries?
                .Select(c => new CountryDTO(
                    c.Cca2,
                    c.Name.Common,
                    c.Flags.Png
                ))
                .OrderBy(c => c.Name)
                .ToList() ?? new List<CountryDTO>();

            return _cachedCountries;
        }
        catch
        {
            return new List<CountryDTO>();
        }
    }

    public async Task<CountryDTO?> GetCountryByCodeAsync(string code)
    {
        IEnumerable<CountryDTO> countries = await GetAllCountriesAsync();
        return countries.FirstOrDefault(c =>
            c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
    }
    #endregion
}
