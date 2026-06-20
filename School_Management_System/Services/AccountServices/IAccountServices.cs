using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace School_Management_System.Services.AccountServices
{
    public interface IAccountServices 
    {
        bool IsLogin(ClaimsPrincipal User);
        Task SendMailAsync(User user, IUrlHelper url, HttpRequest httpRequest, EmailType emailType = EmailType.Register);
    }
}
