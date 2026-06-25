namespace School_Management_System.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IRepository<Student> Student { get; private set; }
        public IRepository<Class> Class { get; private set; }
        public IRepository<Teacher> Teacher { get; private set; }
        public IRepository<UserOTP> UserOtp { get; private set; }
        public  IRepository<TeacherAllocation> TeacherAllocation { get; private set; }
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
           TeacherAllocation =new Repository<TeacherAllocation>(_applicationDbContext);
            UserOtp = new Repository<UserOTP>(_applicationDbContext);
            Teacher = new Repository<Teacher>(_applicationDbContext);
            Class = new Repository<Class>(_applicationDbContext);
            Student = new Repository<Student>(_applicationDbContext);

        }


        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await  _applicationDbContext.SaveChangesAsync();
        }
    }
}
