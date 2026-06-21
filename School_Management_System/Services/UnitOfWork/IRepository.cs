namespace School_Management_System.Services.UnitOfWork
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetOneById(int Id);
    }
}
