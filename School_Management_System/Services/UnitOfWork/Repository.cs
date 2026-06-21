using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace School_Management_System.Services.UnitOfWork
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = applicationDbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? criteria = null,
            params Expression<Func<T, object>>[] include)
        {
            var query = _dbSet.AsQueryable();
            if (criteria != null)
                query = query.Where(criteria);
            if (include != null)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }
        public async Task<T?> GetOneById(int Id)
        {
            var entity = await _dbSet.FindAsync(Id);
            return entity;
        }
    }
}
