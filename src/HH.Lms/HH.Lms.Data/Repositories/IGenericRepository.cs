using HH.Lms.Common.Entity;

namespace HH.Lms.Data.Repositories;

public interface IGenericRepository<T> where T : IBaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    T Add(T entity);
    T Update(T entity);
    void Delete(T entity);
}
