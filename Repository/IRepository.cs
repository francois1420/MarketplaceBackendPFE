using System.Linq.Expressions;

namespace marketplace_backend.Repositories
{
    public interface IRepository<T>
    {
        Task<T?> InsertAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IList<T>> SearchForAsync(Expression<Func<T, bool>> predicate);
        // save entity, test via predicate if entity exists
        Task<bool> SaveAsync(T entity, Expression<Func<T, bool>> predicate);
        Task<IList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
    }
}
