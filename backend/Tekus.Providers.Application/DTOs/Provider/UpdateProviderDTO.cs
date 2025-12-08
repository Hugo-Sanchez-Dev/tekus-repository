namespace Tekus.Providers.Application.DTOs.Provider
{
    public class UpdateProviderDTO()
    {
        public string Nit { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Dictionary<string, object>? CustomFields { get; set; }
    }
}
