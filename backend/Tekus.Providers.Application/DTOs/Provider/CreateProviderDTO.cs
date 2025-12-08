namespace Tekus.Providers.Application.DTOs.Provider
{
    public record CreateProviderDTO(
    string Nit,
    string Name,
    string Email,
    Dictionary<string, object>? CustomFields
    );
}
