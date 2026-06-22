using System.Linq.Expressions;

namespace School_Management_System.Services.UnitOfWork
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<T?> GetOneById(int Id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? criteria = null,
           TypeOfOrder typoOFOrder = TypeOfOrder.Ascending,
           Expression<Func<T, object>>? orderBy = null,
           params Expression<Func<T, object>>[] include);
        Task<T?> GetFirstOne(Expression<Func<T, bool>>? criteria = null,
            TypeOfOrder typoOFOrder = TypeOfOrder.Ascending,
            Expression<Func<T, object>>? orderBy = null,
            params Expression<Func<T, object>>[] include);
    }
}
