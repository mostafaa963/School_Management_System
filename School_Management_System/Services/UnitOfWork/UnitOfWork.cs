namespace School_Management_System.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IRepository<Student> Student { get; private set; }
        public UnitOfWork(IRepository<Student> student, ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext= applicationDbContext;
            Student = student;
        }


        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }

        public int SaveChange()
        {
           return _applicationDbContext.SaveChanges();
        }
    }
}
