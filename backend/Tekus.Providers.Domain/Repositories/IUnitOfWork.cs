namespace Tekus.Providers.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IReportRepository Reports { get; }
        IProviderRepository Providers { get; }
        ICatalogRepository Catalogs { get; }
        IProviderCatalogRepository ProviderCatalogs { get; }
        Task<int> SaveChangesAsync();
    }
}
