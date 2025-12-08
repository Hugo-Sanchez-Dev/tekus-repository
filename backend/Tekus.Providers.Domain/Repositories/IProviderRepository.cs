#region  Usings
using Tekus.Providers.Domain.Entities;


#endregion

namespace Tekus.Providers.Domain.Repositories
{
    public interface IProviderRepository
    {
        Task<Provider?> GetByIdAsync(Guid id);
        Task<IEnumerable<Provider>> GetAllAsync();
        Task<Provider> AddAsync(Provider provider);
        Task UpdateAsync(Provider provider);
        Task DeleteAsync(Guid id);
    }
}
