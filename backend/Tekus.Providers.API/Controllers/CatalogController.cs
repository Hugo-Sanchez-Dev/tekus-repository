#region Usings
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Providers.Application.DTOs.Catalog;
using Tekus.Providers.Application.Enum;
using Tekus.Providers.Application.Extensions;
using Tekus.Providers.Application.Interfaces.Catalog;
#endregion

namespace Tekus.Providers.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    #region Instances
    private readonly ICatalogService _catalogService;
    #endregion

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var catalogs = await _catalogService.GetAllAsync();
        return Ok(catalogs.AsResponseDTO(ResponseCodeEnum.OK));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var catalog = await _catalogService.GetByIdAsync(id);
        if (catalog == null)
            return NotFound();
        return Ok(catalog.AsResponseDTO(ResponseCodeEnum.OK));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CatalogDTO dto)
    {
        var catalog = await _catalogService.CreateAsync(dto);
        return CreatedAtAction(
            nameof(GetById),
            new { id = catalog.Id },
            catalog.AsResponseDTO(ResponseCodeEnum.OK)
            );
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid id, CatalogDTO dto)
    {
        if (id != dto.Id)
            return BadRequest(ResponseCodeEnum.BAD_REQUEST.AsResponseDTO("The URL Id does not match the request body Id."));

        var catalog = await _catalogService.UpdateAsync(id, dto);
        return Ok(catalog.AsResponseDTO(ResponseCodeEnum.OK));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _catalogService.DeleteAsync(id);
        return Ok(ResponseCodeEnum.OK.AsResponseDTO());
    }
}

