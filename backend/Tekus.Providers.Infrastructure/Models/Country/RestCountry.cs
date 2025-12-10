namespace Tekus.Providers.Infrastructure.Models.Country
{
    public class RestCountry
    {
        public string cca2 { get; set; } = "";
        public CountryName name { get; set; } = new();
        public CountryFlags flags { get; set; } = new();
    }
}
