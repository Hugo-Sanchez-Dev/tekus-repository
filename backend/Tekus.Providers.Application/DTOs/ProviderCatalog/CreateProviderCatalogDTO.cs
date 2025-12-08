namespace Tekus.Providers.Application.DTOs.ProviderCatalog
{
    public record CreateProviderCatalogDTO(
    Guid ProviderId,
    Guid CatalogId,
    List<string> Countries
    );
}
