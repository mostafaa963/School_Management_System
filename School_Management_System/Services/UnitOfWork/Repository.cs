using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace School_Management_System.Services.UnitOfWork
{
    public enum TypeOfOrder
    {
        Ascending = 1,
        Descending
    }
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
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? criteria = null,
            bool isNoTracking = false,
            TypeOfOrder typoOFOrder = TypeOfOrder.Ascending,
            Expression<Func<T, object>>? orderBy = null,
            params Expression<Func<T, object>>[] include)
        {
            
            var query = _dbSet.AsQueryable();
            if (isNoTracking)
                query = _dbSet.AsNoTracking().AsQueryable();
            if (criteria != null)
                query = query.Where(criteria);
            if (include != null)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }
            if (orderBy != null)
            {
                if (typoOFOrder == TypeOfOrder.Descending)
                    query = query.OrderByDescending(orderBy);
                else
                    query = query.OrderBy(orderBy);
            }


            return await query.ToListAsync();
        }
        public async Task<T?> GetFirstOne(Expression<Func<T, bool>>? criteria = null,
              bool isNoTracking = false,
            TypeOfOrder typoOFOrder = TypeOfOrder.Ascending,
            Expression<Func<T, object>>? orderBy = null,
            params Expression<Func<T, object>>[] include)
        {
           var entities= await GetAllAsync(criteria, isNoTracking, typoOFOrder, orderBy, include);    
            return entities.FirstOrDefault();
        }
        public async Task<T?> GetOneById(int Id)
        {
            
            var entity = await _dbSet.FindAsync(Id);
             
            return entity;
        }
    }
}
