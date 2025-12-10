namespace Tekus.Providers.Application.DTOs.Country;

public class CountryDTO
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Flag { get; set; }

    public CountryDTO(string code, string name, string flag)
    {
        Code = code;
        Name = name;
        Flag = flag;
    }
}
