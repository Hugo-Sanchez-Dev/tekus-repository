#region Usings
using Tekus.Providers.Application.DTOs.ProviderCatalog;
#endregion

namespace Tekus.Providers.Application.Interfaces.ProviderCatalog
{
    public interface IProviderCatalogService
    {
        Task<IEnumerable<ProviderCatalogDTO>> GetByProviderIdAsync(Guid providerId);
        Task<IEnumerable<ProviderCatalogDTO>> GetByCatalogIdAsync(Guid catalogId);
        Task<ProviderCatalogDTO> CreateAsync(CreateProviderCatalogDTO dto);
        Task<ProviderCatalogDTO> UpdateAsync(Guid id, UpdateProviderCatalogDTO dto);
        Task DeleteAsync(Guid id);
    }
}
