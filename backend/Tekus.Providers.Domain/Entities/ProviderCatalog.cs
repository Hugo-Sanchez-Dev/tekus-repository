namespace Tekus.Providers.Domain.Entities
{
    public class ProviderCatalog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProviderId { get; set; }
        public Guid CatalogId { get; set; }
        public string Countries { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Provider Provider { get; set; }
        public virtual Catalog Catalog { get; set; }

        public ProviderCatalog(Guid providerId, Guid catalogId, string countries)
        {
            Id = Guid.NewGuid();
            ProviderId = providerId;
            CatalogId = catalogId;
            Countries = countries ?? "[]";
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateCountries(string countries)
        {
            Countries = countries ?? "[]";
        }
    }
}
