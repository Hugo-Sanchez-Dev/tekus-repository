#region Usings
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Providers.Application.Enum;
using Tekus.Providers.Application.Extensions;
using Tekus.Providers.Application.Interfaces.Report;
using Tekus.Providers.Domain.Models;
#endregion

namespace Tekus.Providers.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    #region Instances
    private readonly IReportService _reportService;
    #endregion

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("ProviderCatalogRanking")]
    public async Task<IActionResult> GetProviderCatalogRanking()
    {
        IEnumerable<ProviderCatalogRankingResult> report = await _reportService.ProviderCatalogRankingReport();
        return Ok(report.AsResponseDTO(ResponseCodeEnum.OK));
    }

    [HttpGet("CatalogsPerCountry")]
    public async Task<IActionResult> GetCatalogsPerCountry()
    {
        IEnumerable<CountryCatalogsResult> report = await _reportService.CatalogsPerCountryReport();
        return Ok(report.AsResponseDTO(ResponseCodeEnum.OK));
    }
}
