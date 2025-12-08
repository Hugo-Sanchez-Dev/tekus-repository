namespace Tekus.Providers.Application.DTOs.Provider
{
    public record ProviderDTO(
    Guid Id,
    string Nit,
    string Name,
    string Email,
    Dictionary<string, object>? CustomFields,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );
}
