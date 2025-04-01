using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;
public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly DbContext context;
    protected readonly DbSet<T> dbSet;

    protected BaseRepository(DbContext context)
    {
        this.context = context;
        dbSet = this.context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task<bool> AddAsync(T entity)
    {
     await dbSet.AddAsync(entity);
     var affectedRows = await context.SaveChangesAsync();
     return affectedRows > 0;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        var existingEntity = await context.Set<T>().FindAsync(entity);
        if (existingEntity == null)
            return false;

        context.Entry(existingEntity).CurrentValues.SetValues(entity);
        var affectedRows = await context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        dbSet.Remove(entity);
        var affectedRows = await context.SaveChangesAsync();
        return affectedRows > 0;
    }
   
}