#region Usings
using Microsoft.EntityFrameworkCore;
using Tekus.Providers.Domain.Entities;
using Tekus.Providers.Domain.Repositories;
using Tekus.Providers.Infrastructure.Data;
#endregion

namespace Tekus.Providers.Infrastructure.Repositories;

public class ProviderCatalogRepository : IProviderCatalogRepository
{
    private readonly TekusProvidersContext _context;

    public ProviderCatalogRepository(TekusProvidersContext context)
    {
        _context = context;
    }

    public async Task<ProviderCatalog?> GetByIdAsync(Guid id)
    {
        return await _context.ProviderCatalogs
            .Include(ps => ps.Provider)
            .Include(ps => ps.Catalog)
            .FirstOrDefaultAsync(ps => ps.Id == id);
    }

    public async Task<IEnumerable<ProviderCatalog>> GetByProviderIdAsync(Guid providerId)
    {
        return await _context.ProviderCatalogs
            .Include(ps => ps.Provider)
            .Include(ps => ps.Catalog)
            .Where(ps => ps.ProviderId == providerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProviderCatalog>> GetByCatalogIdAsync(Guid serviceId)
    {
        return await _context.ProviderCatalogs
            .Include(ps => ps.Provider)
            .Include(ps => ps.Catalog)
            .Where(ps => ps.CatalogId == serviceId)
            .ToListAsync();
    }

    public async Task<ProviderCatalog> AddAsync(ProviderCatalog ProviderCatalog)
    {
        await _context.ProviderCatalogs.AddAsync(ProviderCatalog);
        return ProviderCatalog;
    }

    public Task UpdateAsync(ProviderCatalog ProviderCatalog)
    {
        _context.ProviderCatalogs.Update(ProviderCatalog);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        ProviderCatalog? ProviderCatalog = await _context.ProviderCatalogs.FindAsync(id);
        if (ProviderCatalog != null)
        {
            _context.ProviderCatalogs.Remove(ProviderCatalog);
        }
    }
}
