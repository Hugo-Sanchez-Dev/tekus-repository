#region Usings
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Providers.Application.DTOs.Provider;
using Tekus.Providers.Application.Enum;
using Tekus.Providers.Application.Extensions;
using Tekus.Providers.Application.Interfaces.Provider;
#endregion

namespace Tekus.Providers.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProviderController : ControllerBase
{
    #region Instances
    private readonly IProviderService _providerService;
    #endregion

    public ProviderController(IProviderService providerService)
    {
        _providerService = providerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<ProviderDTO> providers = await _providerService.GetAllAsync();
        return Ok(providers.AsResponseDTO(ResponseCodeEnum.OK));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        ProviderDTO provider = await _providerService.GetByIdAsync(id);
        if (provider == null)
            return NotFound();
        return Ok(provider.AsResponseDTO(ResponseCodeEnum.OK));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProviderDTO dto)
    {
        ProviderDTO provider = await _providerService.CreateAsync(dto);
        return CreatedAtAction(
            nameof(GetById),
            new { id = provider.Id },
            provider.AsResponseDTO(ResponseCodeEnum.OK));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProviderDTO dto)
    {
        if (id != dto.Id)
            return BadRequest(ResponseCodeEnum.BAD_REQUEST.AsResponseDTO("The URL Id does not match the request body Id."));

        ProviderDTO provider = await _providerService.UpdateAsync(id, dto);
        return Ok(provider.AsResponseDTO(ResponseCodeEnum.OK));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _providerService.DeleteAsync(id);
        return Ok(ResponseCodeEnum.OK.AsResponseDTO());
    }
}