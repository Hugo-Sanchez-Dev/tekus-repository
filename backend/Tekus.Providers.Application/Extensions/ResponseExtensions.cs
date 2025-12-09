#region Usings
using Tekus.Providers.Application.DTOs.Response;
using Tekus.Providers.Application.Enum;
#endregion

namespace Tekus.Providers.Application.Extensions
{
    public static class ResponseExtensions
    {
        public static ResponseDTO<TData> AsResponseDTO<TData>(this TData data, ResponseCodeEnum code, string message = "")
        {
            return new ResponseDTO<TData>()
            {
                Data = data,
                Header = new HeaderDTO()
                {
                    ResponseCode = code
                }
            };
        }

        public static ResponseDTO<object> AsResponseDTO(this ResponseCodeEnum code, string message = "")
        {
            return new ResponseDTO<object>()
            {
                Data = null,
                Header = new HeaderDTO()
                {
                    ResponseCode = code,
                    Message = message
                }
            };
        }
    }
}
