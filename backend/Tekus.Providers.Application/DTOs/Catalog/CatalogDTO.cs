namespace Tekus.Providers.Application.DTOs.Catalog
{
    public record CatalogDTO(
    Guid Id,
    string Name,
    decimal HourlyRate,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );
}
