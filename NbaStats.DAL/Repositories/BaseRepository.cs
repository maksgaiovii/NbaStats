using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly DbContext context;
    protected readonly DbSet<T> dbSet;

    public BaseRepository(DbContext context)
    {
        this.context = context;
        dbSet = this.context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var existingEntity = await context.Set<T>().FindAsync(entity);
        if (existingEntity == null)
            return false;

        context.Entry(existingEntity).CurrentValues.SetValues(entity);
        var affectedRows = await context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await dbSet.FindAsync(id);
        if (entity != null)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}