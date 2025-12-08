namespace Tekus.Providers.Application.DTOs.ProviderCatalog
{
    public record ProviderCatalogDTO(
    Guid Id,
    Guid ProviderId,
    string ProviderName,
    Guid CatalogId,
    string CatalogName,
    List<string> Countries,
    DateTime CreatedAt
    );
}
