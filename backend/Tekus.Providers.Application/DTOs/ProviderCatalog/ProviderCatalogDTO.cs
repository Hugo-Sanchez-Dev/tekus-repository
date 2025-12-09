namespace Tekus.Providers.Application.DTOs.ProviderCatalog;

public class ProviderCatalogDTO()
{
    public Guid Id { get; set; }
    public Guid ProviderId { get; set; }
    public string ProviderName { get; set; }
    public Guid CatalogId { get; set; }
    public string CatalogName { get; set; }
    public List<string> Countries { get; set; }
    public DateTime CreatedAt { get; set; }
}
