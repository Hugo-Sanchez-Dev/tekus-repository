#region Usings
using Price.Core.Services.Interfaces.Generic;
using Tekus.Providers.Application.DTOs.Provider;
#endregion

namespace Tekus.Providers.Application.Interfaces.Provider;

public interface IProviderService : IGenericService<ProviderDTO>
{
}
