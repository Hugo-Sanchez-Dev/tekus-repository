#region Usings
using Tekus.Providers.Application.Interfaces.Report;
using Tekus.Providers.Domain.Models;
using Tekus.Providers.Domain.Repositories;
#endregion

namespace Tekus.Providers.Application.Services;

public class ReportService : IReportService
{
    #region Instances
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    public ReportService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProviderCatalogRankingResult>> ProviderCatalogRankingReport()
    {
        return await _unitOfWork.Reports.ProviderCatalogRankingReport();
    }

    public async Task<IEnumerable<CountryCatalogsResult>> CatalogsPerCountryReport()
    {
        return await _unitOfWork.Reports.CatalogsPerCountryReport();
    }
}
