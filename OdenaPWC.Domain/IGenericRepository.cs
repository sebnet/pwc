using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdenaPWC.Domain
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Delete(T entity);
        void Update(T entity);
    }
}
