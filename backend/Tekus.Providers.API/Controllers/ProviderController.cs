#region Usings
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Providers.Application.DTOs.Provider;
using Tekus.Providers.Application.Interfaces.Provider;
#endregion

namespace Tekus.Providers.API.Controllers
{
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
            try
            {
                var providers = await _providerService.GetAllAsync();
                return Ok(providers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var provider = await _providerService.GetByIdAsync(id);
                if (provider == null)
                    return NotFound();
                return Ok(provider);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProviderDTO dto)
        {
            try
            {
                var provider = await _providerService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = provider.Id }, provider);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProviderDTO dto)
        {
            try
            {
                var provider = await _providerService.UpdateAsync(id, dto);
                return Ok(provider);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _providerService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
