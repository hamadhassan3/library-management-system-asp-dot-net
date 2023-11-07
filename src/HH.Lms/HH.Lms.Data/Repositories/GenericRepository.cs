using HH.Lms.Common.Entity;
using HH.Lms.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MySqlX.XDevAPI.Common;

namespace HH.Lms.Data.Repository;


public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public GenericRepository() { }

    public GenericRepository(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = _dbContext.Set<T>();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual T Add(T entity)
    {
        EntityEntry<T> result = _dbSet.Add(entity);

        if (result == null)
        {
            return null;
        }

        return result.Entity;
    }

    public virtual T Update(T entity)
    {
        var result = _dbSet.Update(entity);
        if (result == null)
        {
            return null;
        }
        return result.Entity;
    }

    public virtual void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
