#region Usings
using AutoMapper;
using System.Text.Json;
using Tekus.Providers.Application.DTOs.Catalog;
using Tekus.Providers.Application.DTOs.Provider;
using Tekus.Providers.Application.DTOs.ProviderCatalog;
using Tekus.Providers.Domain.Entities;
#endregion

namespace Tekus.Providers.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Provider Mappings
            CreateMap<Provider, ProviderDTO>()
                .ForMember(dest => dest.CustomFields, opt => opt.MapFrom(src =>
                    src.CustomFields != null
                    ? JsonSerializer.Deserialize<Dictionary<string, object>>(src.CustomFields, JsonSerializerOptions.Default)
                    : null))
                .ReverseMap()
                .ForMember(dest => dest.CustomFields, opt => opt.MapFrom(src =>
                    src.CustomFields != null
                    ? JsonSerializer.Serialize(src.CustomFields, JsonSerializerOptions.Default)
                    : null));


            CreateMap<CreateProviderDTO, Provider>()
                .ForMember(dest => dest.CustomFields, opt => opt.MapFrom(src =>
                    src.CustomFields != null
                        ? JsonSerializer.Serialize(src.CustomFields, JsonSerializerOptions.Default)
                        : null));

            CreateMap<UpdateProviderDTO, Provider>()
                .ForMember(dest => dest.CustomFields, opt => opt.MapFrom(src =>
                    src.CustomFields != null
                        ? JsonSerializer.Serialize(src.CustomFields, JsonSerializerOptions.Default)
                        : null));
            #endregion

            #region Catalog Mappings
            CreateMap<Catalog, CatalogDTO>()
                .ReverseMap();

            CreateMap<CreateCatalogDTO, Catalog>();

            CreateMap<UpdateCatalogDTO, Catalog>();
            #endregion

            #region ProviderCatalog Mappings
            CreateMap<ProviderCatalog, ProviderCatalogDTO>()
                .ForMember(dest => dest.Countries, opt => opt.MapFrom(src =>
                    src.Countries != null
                    ? JsonSerializer.Deserialize<Dictionary<string, object>>(src.Countries, JsonSerializerOptions.Default)
                    : null))
                .ReverseMap()
                .ForMember(dest => dest.Countries, opt => opt.MapFrom(src =>
                    src.Countries != null && src.Countries.Any()
                        ? JsonSerializer.Serialize(src.Countries, JsonSerializerOptions.Default)
                        : "[]"));

            CreateMap<CreateProviderCatalogDTO, ProviderCatalog>()
                .ForMember(dest => dest.Countries, opt => opt.MapFrom(src =>
                    JsonSerializer.Serialize(src.Countries ?? new List<string>(), JsonSerializerOptions.Default)));

            CreateMap<UpdateProviderCatalogDTO, ProviderCatalog>()
                .ForMember(dest => dest.Countries, opt => opt.MapFrom(src =>
                    JsonSerializer.Serialize(src.Countries ?? new List<string>(), JsonSerializerOptions.Default)));
            #endregion
        }
    }
}