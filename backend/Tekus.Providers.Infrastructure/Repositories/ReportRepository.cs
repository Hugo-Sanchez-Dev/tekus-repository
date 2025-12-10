#region Usings
using Microsoft.EntityFrameworkCore;
using Tekus.Providers.Domain.Models;
using Tekus.Providers.Domain.Repositories;
using Tekus.Providers.Infrastructure.Data; 
#endregion

namespace Tekus.Providers.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly TekusProvidersContext _context;

    public ReportRepository(TekusProvidersContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProviderCatalogRankingResult>> ProviderCatalogRankingReport()
    {
        return await _context.Set<ProviderCatalogRankingResult>()
        .FromSqlInterpolated($"EXEC dbo.sp_GetProviderCatalogsRanking")
        .ToListAsync(); ;
    }

    public async Task<IEnumerable<CountryCatalogsResult>> CatalogsPerCountryReport()
    {
        return await _context.Set<CountryCatalogsResult>()
        .FromSqlInterpolated($"EXEC dbo.sp_GetCatalogsPerCountry")
        .ToListAsync();
    }

}
