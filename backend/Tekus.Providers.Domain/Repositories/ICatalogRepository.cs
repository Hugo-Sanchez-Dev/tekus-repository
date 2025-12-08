#region Usings
using Tekus.Providers.Domain.Entities;
#endregion

namespace Tekus.Providers.Domain.Repositories
{
    public interface ICatalogRepository
    {
        Task<Catalog?> GetByIdAsync(Guid id);
        Task<IEnumerable<Catalog>> GetAllAsync();
        Task<Catalog> AddAsync(Catalog catalog);
        Task UpdateAsync(Catalog catalog);
        Task DeleteAsync(Guid id);
    }
}
