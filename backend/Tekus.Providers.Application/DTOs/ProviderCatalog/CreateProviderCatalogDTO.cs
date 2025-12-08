namespace Tekus.Providers.Application.DTOs.ProviderCatalog
{
    public class CreateProviderCatalogDTO()
    {
        public Guid ProviderId { get; set; }
        public Guid CatalogId { get; set; }
        public List<string> Countries { get; set; }
    }
}
