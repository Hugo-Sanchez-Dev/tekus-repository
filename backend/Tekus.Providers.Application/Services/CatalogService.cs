#region Usings
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CatalogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CatalogDTO>> GetAllAsync()
        {
            IEnumerable<Catalog> catalogs = await _unitOfWork.Catalogs.GetAllAsync();
            return _mapper.Map<IEnumerable<CatalogDTO>>(catalogs);
        }

        public async Task<CatalogDTO?> GetByIdAsync(Guid id)
        {
            Catalog? catalog = await _unitOfWork.Catalogs.GetByIdAsync(id);
            return catalog == null ? null : _mapper.Map<CatalogDTO>(catalog);
        }

        public async Task<CatalogDTO> CreateAsync(CatalogDTO dto)
        {
            Catalog catalog = new Catalog(dto.Name, dto.HourlyRate);
            await _unitOfWork.Catalogs.AddAsync(catalog);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CatalogDTO>(catalog);
        }

        public async Task<CatalogDTO> UpdateAsync(Guid id, CatalogDTO dto)
        {
            Catalog? catalog = await _unitOfWork.Catalogs.GetByIdAsync(id);
            if (catalog == null)
                throw new KeyNotFoundException($"Catalog {id} not found");

            catalog.Update(dto.Name, dto.HourlyRate);
            await _unitOfWork.Catalogs.UpdateAsync(catalog);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CatalogDTO>(catalog);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Catalogs.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
