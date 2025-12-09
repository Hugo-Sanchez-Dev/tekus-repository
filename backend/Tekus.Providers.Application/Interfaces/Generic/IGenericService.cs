namespace Price.Core.Services.Interfaces.Generic;

public interface IGenericService<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T data);
    Task<T> UpdateAsync(Guid id, T data);
    Task DeleteAsync(Guid id);
}
