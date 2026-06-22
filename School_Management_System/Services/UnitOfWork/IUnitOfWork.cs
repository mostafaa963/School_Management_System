namespace School_Management_System.Services.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<Student> Student { get; }
        IRepository<UserOTP> UserOtp { get; }
        int SaveChange();
    }
}
