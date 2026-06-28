using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using School_Management_System.DataAccess;
using School_Management_System.MiddleWare;
using School_Management_System.Models;
using School_Management_System.Services;
using School_Management_System.Utilities;

namespace School_Management_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // option pattern 
            builder.Services.Configure<AdminInfo>(builder.Configuration.GetSection("AdminInfo"));
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequireUppercase = true;
                option.Password.RequireLowercase = true;
                option.User.RequireUniqueEmail = true;
                option.SignIn.RequireConfirmedEmail = true;
                option.Lockout.AllowedForNewUsers = true;


            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = $"/Identity/Account/LogIn";
            });

            builder.Services.Configure();
            //builder.Services.AddRateLimiter(options =>
            //{
            //    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            //    options.AddFixedWindowLimiter("MyCustomPolicy", opt =>
            //    {
            //        opt.PermitLimit = 5;
            //        opt.Window = TimeSpan.FromSeconds(10);
            //        opt.QueueLimit = 0;
            //    });
            //});

            var app = builder.Build();

            await SeedServices.SeedData(app.Services);
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.MapStaticAssets();
            app.UseHttpsRedirection();

            //app.UseRateLimiter();
            app.UseMiddleware<LimitingRequest>();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Student}/{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
