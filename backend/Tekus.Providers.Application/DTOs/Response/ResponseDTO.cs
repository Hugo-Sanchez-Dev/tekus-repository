using SocialMedia.Core.CustomEntities;

namespace Tekus.Providers.Application.DTOs.Response
{
    public record ResponseDTO<T>
    {
        public ResponseDTO(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public Metadata Meta { get; set; }
    }
}
