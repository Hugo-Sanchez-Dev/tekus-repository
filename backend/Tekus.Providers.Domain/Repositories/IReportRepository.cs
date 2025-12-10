#region Usings
using Tekus.Providers.Domain.Models; 
#endregion

namespace Tekus.Providers.Domain.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<ProviderCatalogRankingResult>> ProviderCatalogRankingReport();
    Task<IEnumerable<CountryCatalogsResult>> CatalogsPerCountryReport();
}