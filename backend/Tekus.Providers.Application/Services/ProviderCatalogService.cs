#region Usings
using System.Text.Json;
using Tekus.Providers.Application.DTOs.ProviderCatalog;
using Tekus.Providers.Application.Interfaces.ProviderCatalog;
using Tekus.Providers.Domain.Entities;
using Tekus.Providers.Domain.Repositories;
#endregion

namespace Tekus.Providers.Application.Services
{
    public class ProviderCatalogService : IProviderCatalogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProviderCatalogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProviderCatalogDTO>> GetByProviderIdAsync(Guid providerId)
        {
            IEnumerable<ProviderCatalog> ProviderCatalogs = await _unitOfWork.ProviderCatalogs.GetByProviderIdAsync(providerId);
            return ProviderCatalogs.Select(MapToDto);
        }

        public async Task<IEnumerable<ProviderCatalogDTO>> GetByCatalogIdAsync(Guid CatalogId)
        {
            IEnumerable<ProviderCatalog> ProviderCatalogs = await _unitOfWork.ProviderCatalogs.GetByCatalogIdAsync(CatalogId);
            return ProviderCatalogs.Select(MapToDto);
        }

        public async Task<ProviderCatalogDTO> CreateAsync(CreateProviderCatalogDTO dto)
        {
            string countriesJson = JsonSerializer.Serialize(dto.Countries);
            ProviderCatalog providerCatalog = new ProviderCatalog(dto.ProviderId, dto.CatalogId, countriesJson);

            await _unitOfWork.ProviderCatalogs.AddAsync(providerCatalog);
            await _unitOfWork.SaveChangesAsync();

            ProviderCatalog? created = await _unitOfWork.ProviderCatalogs.GetByIdAsync(providerCatalog.Id);
            return MapToDto(created!);
        }

        public async Task<ProviderCatalogDTO> UpdateAsync(Guid id, UpdateProviderCatalogDTO dto)
        {
            ProviderCatalog? providerCatalog = await _unitOfWork.ProviderCatalogs.GetByIdAsync(id);
            if (providerCatalog == null)
                throw new KeyNotFoundException($"providerCatalog {id} not found");

            string countriesJson = JsonSerializer.Serialize(dto.Countries);
            providerCatalog.UpdateCountries(countriesJson);

            await _unitOfWork.ProviderCatalogs.UpdateAsync(providerCatalog);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(providerCatalog);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.ProviderCatalogs.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        private static ProviderCatalogDTO MapToDto(ProviderCatalog ps)
        {
            List<string> countries = JsonSerializer.Deserialize<List<string>>(ps.Countries) ?? new List<string>();

            return new ProviderCatalogDTO(
                ps.Id,
                ps.ProviderId,
                ps.Provider?.Name ?? "",
                ps.CatalogId,
                ps.Catalog?.Name ?? "",
                countries,
                ps.CreatedAt
            );
        }
    }
}