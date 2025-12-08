namespace Tekus.Providers.Application.DTOs.Provider
{
    public record UpdateProviderDTO(
    string Nit,
    string Name,
    string Email,
    Dictionary<string, object>? CustomFields
    );
}
