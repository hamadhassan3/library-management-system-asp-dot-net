using HH.Lms.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Lms.Data.Repositories
{
    internal interface IGenericRepository<T> where T : IBaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
