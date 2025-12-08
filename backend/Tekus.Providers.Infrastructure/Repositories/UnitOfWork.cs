#region Usings
using Tekus.Providers.Domain.Repositories;
using Tekus.Providers.Infrastructure.Data;
#endregion

namespace Tekus.Providers.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TekusProvidersContext _context;
        private IProviderRepository? _providers;
        private ICatalogRepository? _catalogs;
        private IProviderCatalogRepository? _providerCatalogs;

        public UnitOfWork(TekusProvidersContext context)
        {
            _context = context;
        }

        public IProviderRepository Providers =>
            _providers ??= new ProviderRepository(_context);

        public ICatalogRepository Catalogs =>
            _catalogs ??= new CatalogRepository(_context);

        public IProviderCatalogRepository ProviderCatalogs =>
            _providerCatalogs ??= new ProviderCatalogRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
