namespace Tekus.Providers.Application.DTOs.Provider
{
    public class ProviderDTO()
    {
        public Guid Id { get; set; }
        public string Nit { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Dictionary<string, object>? CustomFields { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
