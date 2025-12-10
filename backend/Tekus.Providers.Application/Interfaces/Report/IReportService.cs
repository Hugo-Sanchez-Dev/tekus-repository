using Tekus.Providers.Domain.Models;

namespace Tekus.Providers.Application.Interfaces.Report;

public interface IReportService
{
    Task<IEnumerable<ProviderCatalogRankingResult>> ProviderCatalogRankingReport();
    Task<IEnumerable<CountryCatalogsResult>> CatalogsPerCountryReport();
}
