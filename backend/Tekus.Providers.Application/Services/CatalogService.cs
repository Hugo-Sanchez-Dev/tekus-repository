#region Usings
using Tekus.Providers.Application.DTOs.Catalog;
using Tekus.Providers.Application.Interfaces.Catalog;
using Tekus.Providers.Domain.Entities;
using Tekus.Providers.Domain.Repositories;
#endregion

namespace Tekus.Providers.Application.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatalogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CatalogDTO>> GetAllAsync()
        {
            IEnumerable<Catalog> services = await _unitOfWork.Catalogs.GetAllAsync();
            return services.Select(MapToDto);
        }

        public async Task<CatalogDTO?> GetByIdAsync(Guid id)
        {
            Catalog? service = await _unitOfWork.Catalogs.GetByIdAsync(id);
            return service == null ? null : MapToDto(service);
        }

        public async Task<CatalogDTO> CreateAsync(CreateCatalogDTO dto)
        {
            Catalog service = new Catalog(dto.Name, dto.HourlyRate);
            await _unitOfWork.Catalogs.AddAsync(service);
            await _unitOfWork.SaveChangesAsync();
            return MapToDto(service);
        }

        public async Task<CatalogDTO> UpdateAsync(Guid id, UpdateCatalogDTO dto)
        {
            Catalog? service = await _unitOfWork.Catalogs.GetByIdAsync(id);
            if (service == null)
                throw new KeyNotFoundException($"Service {id} not found");

            service.Update(dto.Name, dto.HourlyRate);
            await _unitOfWork.Catalogs.UpdateAsync(service);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(service);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Catalogs.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        private static CatalogDTO MapToDto(Catalog service)
        {
            return new CatalogDTO(
                service.Id,
                service.Name,
                service.HourlyRate,
                service.CreatedAt,
                service.UpdatedAt
            );
        }
    }
}
