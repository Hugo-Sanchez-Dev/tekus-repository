namespace Tekus.Providers.Domain.Entities
{
    public class Provider
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nit { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? CustomFields { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ProviderCatalog> ProviderCatalog { get; set; }

        public Provider(string nit, string name, string email)
        {
            Id = Guid.NewGuid();
            Nit = nit ?? throw new ArgumentNullException(nameof(nit));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            CreatedAt = DateTime.UtcNow;
            ProviderCatalog = new List<ProviderCatalog>();
        }

        public void Update(string nit, string name, string email, string? customFields)
        {
            Nit = nit;
            Name = name;
            Email = email;
            CustomFields = customFields;
            UpdatedAt = DateTime.UtcNow;
        }
        public void SetCustomFields(string customFieldsJson)
        {
            CustomFields = customFieldsJson;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
