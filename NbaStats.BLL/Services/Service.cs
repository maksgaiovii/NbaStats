using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class Service<T> : IService<T> where T : class
{
    protected readonly IRepository<T> repository;

    public Service(IRepository<T> repository)
    {
        this.repository = repository;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }

    public virtual async Task<T?> AddAsync(T entity)
    {
        return await repository.AddAsync(entity);
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        return await repository.UpdateAsync(entity);
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        return await repository.DeleteAsync(entity);
    }
}
    