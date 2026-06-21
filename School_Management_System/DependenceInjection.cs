using Microsoft.AspNetCore.Identity.UI.Services;
using School_Management_System.Services.AccountServices;
using School_Management_System.Services.UnitOfWork;
using School_Management_System.Utilities;

namespace School_Management_System
{
    public static class DependenceInjection
    {
        public static void Configure(this IServiceCollection services)
        {
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IEmailSender,EmailSender>();
            services.AddScoped<IRepository<Student>, Repository<Student>>();
        }
    }
}
