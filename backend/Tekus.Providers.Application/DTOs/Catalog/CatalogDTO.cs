namespace Tekus.Providers.Application.DTOs.Catalog
{
    public class CatalogDTO()
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
