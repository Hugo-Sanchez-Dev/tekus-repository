#region Usings
using Microsoft.EntityFrameworkCore;
using Tekus.Providers.Domain.Entities;
using Tekus.Providers.Domain.Repositories;
using Tekus.Providers.Infrastructure.Data;
#endregion

namespace Tekus.Providers.Infrastructure.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly TekusProvidersContext _context;

        public ProviderRepository(TekusProvidersContext context)
        {
            _context = context;
        }

        public async Task<Provider?> GetByIdAsync(Guid id)
        {
            return await _context.Providers
                .Include(p => p.ProviderCatalog)
                .ThenInclude(ps => ps.Catalog)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Provider>> GetAllAsync()
        {
            return await _context.Providers
                .Include(p => p.ProviderCatalog)
                .ThenInclude(ps => ps.Catalog)
                .ToListAsync();
        }

        public async Task<Provider> AddAsync(Provider provider)
        {
            await _context.Providers.AddAsync(provider);
            return provider;
        }

        public Task UpdateAsync(Provider provider)
        {
            _context.Providers.Update(provider);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            Provider? provider = await _context.Providers.FindAsync(id);
            if (provider != null)
            {
                _context.Providers.Remove(provider);
            }
        }
    }
}
