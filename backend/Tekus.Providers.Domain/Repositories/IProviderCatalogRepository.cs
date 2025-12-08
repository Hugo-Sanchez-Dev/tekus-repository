#region Usings
using Tekus.Providers.Domain.Entities;

#endregion

namespace Tekus.Providers.Domain.Repositories
{
    public interface IProviderCatalogRepository
    {
        Task<ProviderCatalog?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProviderCatalog>> GetByProviderIdAsync(Guid providerId);
        Task<IEnumerable<ProviderCatalog>> GetByCatalogIdAsync(Guid catalogId);
        Task<ProviderCatalog> AddAsync(ProviderCatalog providerCatalog);
        Task UpdateAsync(ProviderCatalog providerCatalog);
        Task DeleteAsync(Guid id);
    }
}
