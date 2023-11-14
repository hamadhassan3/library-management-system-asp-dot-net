using HH.Lms.Common.Entity;
using HH.Lms.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MySqlX.XDevAPI.Common;
using System.Linq.Expressions;

namespace HH.Lms.Data.Repository;


public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository() { }

    public GenericRepository(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = _dbContext.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> Find(List<Expression<Func<T, bool>>> predicates)
    {
        IQueryable<T> query = _dbSet;

        foreach (var predicate in predicates)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
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
