namespace Tekus.Providers.Infrastructure.Models.Country
{
    public class RestCountry
    {
        public string Cca2 { get; set; } = "";
        public CountryName Name { get; set; } = new();
        public CountryFlags Flags { get; set; } = new();
    }
}
