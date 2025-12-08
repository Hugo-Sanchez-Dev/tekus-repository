#region Usings
using Tekus.Providers.Application.DTOs.Country;
#endregion

namespace Tekus.Providers.Application.Interfaces.Country
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetAllCountriesAsync();
        Task<CountryDTO?> GetCountryByCodeAsync(string code);
    }
}
