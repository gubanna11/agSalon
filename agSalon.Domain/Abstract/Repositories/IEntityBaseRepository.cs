using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Domain.Abstract.Repositories
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        //Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();

		Task<IReadOnlyList<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
