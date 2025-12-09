namespace Tekus.Providers.Application.DTOs.Response;

public class ResponseDTO<TData>
{
    private HeaderDTO _header;
    public HeaderDTO Header
    {
        get
        {
            if (_header == null)
                _header = new HeaderDTO();

            return _header;
        }
        set
        {
            _header = value;
        }
    }
    public TData Data { get; set; }
}