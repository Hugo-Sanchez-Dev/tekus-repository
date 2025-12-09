#region Usings
using Price.Core.Services.Interfaces.Generic;
using Tekus.Providers.Application.DTOs.Catalog;
#endregion

namespace Tekus.Providers.Application.Interfaces.Catalog;

public interface ICatalogService : IGenericService<CatalogDTO>
{
}
