#region Usings
namespace Tekus.Providers.Domain.Models; 
#endregion

public class CountryCatalogsResult
{
    public string Country { get; set; } = string.Empty;
    public int CatalogQuantity { get; set; }
}