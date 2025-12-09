#region Usings
using AutoMapper;
using System.Text.Json;
using Tekus.Providers.Application.DTOs.ProviderCatalog;
using Tekus.Providers.Application.Interfaces.ProviderCatalog;
using Tekus.Providers.Domain.Entities;
using Tekus.Providers.Domain.Repositories;
#endregion

namespace Tekus.Providers.Application.Services;

public class ProviderCatalogService : IProviderCatalogService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProviderCatalogService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProviderCatalogDTO>> GetByProviderIdAsync(Guid providerId)
    {
        IEnumerable<ProviderCatalog> providerCatalogs = await _unitOfWork.ProviderCatalogs.GetByProviderIdAsync(providerId);
        return _mapper.Map<IEnumerable<ProviderCatalogDTO>>(providerCatalogs);
    }

    public async Task<IEnumerable<ProviderCatalogDTO>> GetByCatalogIdAsync(Guid CatalogId)
    {
        IEnumerable<ProviderCatalog> providerCatalogs = await _unitOfWork.ProviderCatalogs.GetByCatalogIdAsync(CatalogId);
        return _mapper.Map<IEnumerable<ProviderCatalogDTO>>(providerCatalogs);
    }

    public async Task<ProviderCatalogDTO> CreateAsync(ProviderCatalogDTO dto)
    {
        string countriesJson = JsonSerializer.Serialize(dto.Countries);
        ProviderCatalog providerCatalog = new ProviderCatalog(dto.ProviderId, dto.CatalogId, countriesJson);

        await _unitOfWork.ProviderCatalogs.AddAsync(providerCatalog);
        await _unitOfWork.SaveChangesAsync();

        ProviderCatalog? created = await _unitOfWork.ProviderCatalogs.GetByIdAsync(providerCatalog.Id);
        return _mapper.Map<ProviderCatalogDTO>(created);
    }

    public async Task<ProviderCatalogDTO> UpdateAsync(ProviderCatalogDTO dto)
    {
        ProviderCatalog? providerCatalog = await _unitOfWork.ProviderCatalogs.GetByIdAsync(dto.Id);
        if (providerCatalog == null)
            throw new KeyNotFoundException($"providerCatalog {dto.Id} not found");

        string countriesJson = JsonSerializer.Serialize(dto.Countries);
        providerCatalog.UpdateCountries(countriesJson);

        await _unitOfWork.ProviderCatalogs.UpdateAsync(providerCatalog);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProviderCatalogDTO>(providerCatalog);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _unitOfWork.ProviderCatalogs.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public Task<IEnumerable<ProviderCatalogDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProviderCatalogDTO> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}