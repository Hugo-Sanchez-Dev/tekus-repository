namespace Tekus.Providers.Domain.Entities
{
    public class Catalog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ProviderCatalog> ProviderCatalog { get; set; }

        public Catalog(string name, decimal hourlyRate)
        {
            Id = Guid.NewGuid();
            Name = name;
            HourlyRate = hourlyRate;
            CreatedAt = DateTime.UtcNow;
            ProviderCatalog = new List<ProviderCatalog>();
        }

        public void Update(string name, decimal hourlyRate)
        {
            Name = name;
            HourlyRate = hourlyRate;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}