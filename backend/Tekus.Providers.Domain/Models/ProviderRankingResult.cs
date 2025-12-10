#region Usings
namespace Tekus.Providers.Domain.Models; 
#endregion

public class ProviderCatalogRankingResult
{
    public long Position { get; set; }
    public string ProviderName { get; set; } = string.Empty;
    public int CatalogQuantity { get; set; }
}
