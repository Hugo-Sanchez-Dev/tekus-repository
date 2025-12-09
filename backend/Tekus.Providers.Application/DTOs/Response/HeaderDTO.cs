using Tekus.Providers.Application.Enum;

namespace Tekus.Providers.Application.DTOs.Response;

public class HeaderDTO
{
    public ResponseCodeEnum ResponseCode { get; set; }
    public string Message { get; set; }
    public bool Success
    {
        get
        {
            int responseCode = (int)ResponseCode;
            if (responseCode >= 200 && responseCode < 300)
                return true;

            return false;
        }
    }
}