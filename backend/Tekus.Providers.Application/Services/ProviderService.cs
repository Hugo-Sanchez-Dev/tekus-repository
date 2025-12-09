#region Usings
using AutoMapper;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Tekus.Providers.Application.DTOs.Provider;
using Tekus.Providers.Application.Interfaces.Provider;
using Tekus.Providers.Domain.Entities;
using Tekus.Providers.Domain.Repositories;
#endregion

namespace Tekus.Providers.Application.Services;

public class ProviderService : IProviderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<ProviderDTO> _validator;

    public ProviderService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ProviderDTO> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<IEnumerable<ProviderDTO>> GetAllAsync()
    {
        IEnumerable<Provider> providers = await _unitOfWork.Providers.GetAllAsync();
        return _mapper.Map<IEnumerable<ProviderDTO>>(providers);
    }

    public async Task<ProviderDTO?> GetByIdAsync(Guid id)
    {
        Provider? provider = await _unitOfWork.Providers.GetByIdAsync(id);
        return provider == null ? null : _mapper.Map<ProviderDTO>(provider);
    }

    public async Task<ProviderDTO> CreateAsync(ProviderDTO dto)
    {
        string? customFieldsJson = dto.CustomFields != null
            ? JsonSerializer.Serialize(dto.CustomFields)
            : null;

        Provider provider = new Provider(dto.Nit, dto.Name, dto.Email);
        if (customFieldsJson != null)
            provider.SetCustomFields(customFieldsJson);

        await _unitOfWork.Providers.AddAsync(provider);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProviderDTO>(provider);
    }

    public async Task<ProviderDTO> UpdateAsync(Guid id, ProviderDTO dto)
    {
        Provider? provider = await _unitOfWork.Providers.GetByIdAsync(dto.Id);
        if (provider == null)
            throw new KeyNotFoundException($"Provider {dto.Id} not found");

        string? customFieldsJson = dto.CustomFields != null
            ? JsonSerializer.Serialize(dto.CustomFields)
            : null;

        provider.Update(dto.Nit, dto.Name, dto.Email, customFieldsJson);
        await _unitOfWork.Providers.UpdateAsync(_mapper.Map<Provider>(dto));
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProviderDTO>(provider);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _unitOfWork.Providers.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}
