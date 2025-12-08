#region Usings
using Tekus.Providers.Application.DTOs.Provider;
#endregion

namespace Tekus.Providers.Application.Interfaces.Provider
{
    public interface IProviderService
    {
        Task<IEnumerable<ProviderDTO>> GetAllAsync();
        Task<ProviderDTO?> GetByIdAsync(Guid id);
        Task<ProviderDTO> CreateAsync(CreateProviderDTO dto);
        Task<ProviderDTO> UpdateAsync(Guid id, UpdateProviderDTO dto);
        Task DeleteAsync(Guid id);
    }
}
