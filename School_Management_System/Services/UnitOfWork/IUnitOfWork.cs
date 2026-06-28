namespace School_Management_System.Services.UnitOfWork
{
    
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Student { get; }
        IRepository<Subject> Subject { get; }
        IRepository<Class> Class { get; }
        IRepository<Teacher> Teacher { get; }
        IRepository<UserOTP> UserOtp { get; }
        IRepository<TeacherAllocation> TeacherAllocation { get; }
        Task<int> SaveChangeAsync();
    }
}
