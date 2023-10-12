using System.Linq.Expressions;

namespace BharatMedicsV2.Repository.IRepository
{
    public interface ICommonRepository <T> where T : class
    {
        Task CreateAsync(T entity );
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);

        Task<T> GetAsync(Expression<Func<T, bool>>? predicate = null, bool tracked = true);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }
}
