#region Usings
using System.Text.Json;
using Tekus.Providers.Application.DTOs.Provider;
using Tekus.Providers.Application.Interfaces.Provider;
using Tekus.Providers.Domain.Entities;
using Tekus.Providers.Domain.Repositories;
#endregion

namespace Tekus.Providers.Application.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProviderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProviderDTO>> GetAllAsync()
        {
            IEnumerable<Provider> providers = await _unitOfWork.Providers.GetAllAsync();
            return providers.Select(MapToDto);
        }

        public async Task<ProviderDTO?> GetByIdAsync(Guid id)
        {
            Provider? provider = await _unitOfWork.Providers.GetByIdAsync(id);
            return provider == null ? null : MapToDto(provider);
        }

        public async Task<ProviderDTO> CreateAsync(CreateProviderDTO dto)
        {
            string? customFieldsJson = dto.CustomFields != null
                ? JsonSerializer.Serialize(dto.CustomFields)
                : null;

            Provider provider = new Provider(dto.Nit, dto.Name, dto.Email);
            if (customFieldsJson != null)
                provider.SetCustomFields(customFieldsJson);

            await _unitOfWork.Providers.AddAsync(provider);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(provider);
        }

        public async Task<ProviderDTO> UpdateAsync(Guid id, UpdateProviderDTO dto)
        {
            Provider? provider = await _unitOfWork.Providers.GetByIdAsync(id);
            if (provider == null)
                throw new KeyNotFoundException($"Provider {id} not found");

            string? customFieldsJson = dto.CustomFields != null
                ? JsonSerializer.Serialize(dto.CustomFields)
                : null;

            provider.Update(dto.Nit, dto.Name, dto.Email, customFieldsJson);
            await _unitOfWork.Providers.UpdateAsync(provider);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(provider);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Providers.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        private static ProviderDTO MapToDto(Provider provider)
        {
            Dictionary<string, object>? customFields = null;
            if (!string.IsNullOrEmpty(provider.CustomFields))
            {
                customFields = JsonSerializer.Deserialize<Dictionary<string, object>>(provider.CustomFields);
            }

            return new ProviderDTO(
                provider.Id,
                provider.Nit,
                provider.Name,
                provider.Email,
                customFields,
                provider.CreatedAt,
                provider.UpdatedAt
            );
        }
    }
}
