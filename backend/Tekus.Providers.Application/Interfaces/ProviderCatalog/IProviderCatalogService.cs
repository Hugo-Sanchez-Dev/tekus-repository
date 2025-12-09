#region Usings
using Price.Core.Services.Interfaces.Generic;
using Tekus.Providers.Application.DTOs.ProviderCatalog;
#endregion

namespace Tekus.Providers.Application.Interfaces.ProviderCatalog;

public interface IProviderCatalogService : IGenericService<ProviderCatalogDTO>
{
    Task<IEnumerable<ProviderCatalogDTO>> GetByProviderIdAsync(Guid providerId);
    Task<IEnumerable<ProviderCatalogDTO>> GetByCatalogIdAsync(Guid catalogId);
}
