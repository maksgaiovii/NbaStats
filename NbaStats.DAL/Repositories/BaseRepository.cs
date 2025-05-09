﻿using Microsoft.EntityFrameworkCore;
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

    public virtual async Task<T?> AddAsync(T entity)
    {
    try
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        return null;
    }
    return entity;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        var entry = context.Entry(entity);
        if (entry.State == EntityState.Detached)
        {
            dbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }
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