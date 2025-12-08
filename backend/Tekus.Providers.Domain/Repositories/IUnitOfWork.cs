namespace Tekus.Providers.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IProviderRepository Providers { get; }
        ICatalogRepository Catalogs { get; }
        IProviderCatalogRepository ProviderCatalogs { get; }
        Task<int> SaveChangesAsync();
    }
}
