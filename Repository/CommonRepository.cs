using BharatMedicsV2.DataFiles;
using BharatMedicsV2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BharatMedicsV2.Repository
{
    public class CommonRepository <T> : ICommonRepository <T> where T : class
    {
        private readonly DataContext _dataContext;
        internal DbSet<T> dbSet;

        public CommonRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            this.dbSet = _dataContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        

        public async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

        public async Task<T>GetAsync(Expression<Func<T, bool>>? predicate, bool tracked)
        {
            IQueryable<T> values = dbSet;
            if (!tracked)
            {
                values = values.AsNoTracking();
            }
            if (predicate != null)
            {
                values = values.Where(predicate);
            }

            return await values.FirstOrDefaultAsync();
        }
    }
}
