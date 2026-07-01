using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using School_Management_System.DataAccess;
using School_Management_System.Models;
using School_Management_System.Utilities;

namespace School_Management_System.Services
{
    public class SeedServices
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            //Service Locator pattern:
            using var Scope = serviceProvider.CreateScope();
            var context = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManger = Scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var logger = Scope.ServiceProvider.GetRequiredService<ILogger<SeedServices>>();
            var configure = Scope.ServiceProvider.GetRequiredService<IOptions<AdminInfo>>();

            try
            {
                //Ensure the database is read
                logger.LogInformation("Ensure the database is read");
                await context.Database.EnsureCreatedAsync();
                //Add Role 
                logger.LogInformation("AddRoles");
                await AddRoleAsync(roleManager, RolesNames.ADMIN_ROLES);
                await AddRoleAsync(roleManager, RolesNames.TEACHER_ROLES);
                await AddRoleAsync(roleManager, RolesNames.STUDENT_ROLES);
                //await context.SaveChangesAsync();

                string  Email = configure.Value.Email;
                //create User 
                if (await userManger.FindByEmailAsync(Email) is null)
                {

                   var user = new User
                    {
                        IsActive = true,
                        FullName = configure.Value.Name,
                        UserName = configure.Value.Name,
                        Email = configure.Value.Email,
                        EmailConfirmed = true,
                    };
                  var result = await userManger.CreateAsync(user, configure.Value.Password);
                        logger.LogInformation("create Admin User Successfully");

                    if (result.Succeeded)
                    {
                        logger.LogInformation("Add Admin User Successfully");
                        await userManger.AddToRoleAsync(user, RolesNames.ADMIN_ROLES);
                    }
                    else
                    {
                        logger.LogError($"Error is {string.Join(",", result.Errors.Select(e => e.Description))}");
                    }
                    //await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Error Accrued in DataBase message{ex.Message}");

            }
        }
        public static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName,
                });
                if (!result.Succeeded)
                {
                    throw new Exception($"failed to create role {roleName} ,Error {string.Join(" ", result.Errors.Select(e => e.Description))} ");
                }

            }
        }
    }
}
