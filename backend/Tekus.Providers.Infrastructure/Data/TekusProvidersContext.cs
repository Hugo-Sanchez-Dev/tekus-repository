#region Usings
using Microsoft.EntityFrameworkCore;
using Tekus.Providers.Domain.Entities;
using Tekus.Providers.Domain.Models;
#endregion

namespace Tekus.Providers.Infrastructure.Data
{
    public class TekusProvidersContext : DbContext
    {
        public TekusProvidersContext(DbContextOptions<TekusProvidersContext> options)
            : base(options)
        {
        }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<ProviderCatalog> ProviderCatalogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProviderCatalog>()
                .HasKey(ps => new { ps.ProviderId, ps.CatalogId });

            modelBuilder.Entity<ProviderCatalog>()
                .HasOne(ps => ps.Provider)
                .WithMany(p => p.ProviderCatalog)
                .HasForeignKey(ps => ps.ProviderId);

            modelBuilder.Entity<ProviderCatalog>()
                .HasOne(ps => ps.Catalog)
                .WithMany(s => s.ProviderCatalog)
                .HasForeignKey(ps => ps.CatalogId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProviderCatalogRankingResult>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            modelBuilder.Entity<CountryCatalogsResult>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });
        }
    }
}
