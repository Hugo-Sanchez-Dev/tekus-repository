using Tekus.Providers.Application.DTOs.Catalog;

namespace Tekus.Providers.Application.Interfaces.Catalog
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogDTO>> GetAllAsync();
        Task<CatalogDTO?> GetByIdAsync(Guid id);
        Task<CatalogDTO> CreateAsync(CreateCatalogDTO dto);
        Task<CatalogDTO> UpdateAsync(Guid id, UpdateCatalogDTO dto);
        Task DeleteAsync(Guid id);
    }
}
