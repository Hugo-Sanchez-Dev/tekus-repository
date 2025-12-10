#region Usings
using Microsoft.EntityFrameworkCore;
using Tekus.Providers.Domain.Entities;
using Tekus.Providers.Domain.Repositories;
using Tekus.Providers.Infrastructure.Data;
#endregion

namespace Tekus.Providers.Infrastructure.Repositories;

public class CatalogRepository : ICatalogRepository
{
    private readonly TekusProvidersContext _context;

    public CatalogRepository(TekusProvidersContext context)
    {
        _context = context;
    }

    public async Task<Catalog?> GetByIdAsync(Guid id)
    {
        return await _context.Catalogs
            .AsNoTracking()
            .Include(s => s.ProviderCatalog)
            .ThenInclude(ps => ps.Provider)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Catalog>> GetAllAsync()
    {
        return await _context.Catalogs
            .Include(s => s.ProviderCatalog)
            .ThenInclude(ps => ps.Provider)
            .ToListAsync();
    }

    public async Task<Catalog> AddAsync(Catalog service)
    {
        await _context.Catalogs.AddAsync(service);
        return service;
    }

    public Task UpdateAsync(Catalog service)
    {
        _context.Catalogs.Update(service);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        Catalog? service = await _context.Catalogs.FindAsync(id);
        if (service != null)
        {
            _context.Catalogs.Remove(service);
        }
    }
}
