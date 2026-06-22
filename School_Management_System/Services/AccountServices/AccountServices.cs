using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Services.UnitOfWork;
using School_Management_System.Utilities;
using Stripe.Reporting;
using System.Security.Claims;

namespace School_Management_System.Services.AccountServices
{
    public enum EmailType
    {
        Register,
        ResendConfirmation,
        ForgetPassword
    }
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;

        public AccountServices(IUnitOfWork unitOfWork, UserManager<User> userManager, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public bool IsLogin(ClaimsPrincipal? User)
        {
            return User is not null && User.Identity!.IsAuthenticated;
        }

        public async Task SendMailAsync(User user, IUrlHelper url, HttpRequest httpRequest, EmailType emailType = EmailType.Register)
        {
            //generate token 
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var Url = url.Action("Confirm", "Account",
                new { area = SD.ADENTITY_AREA, token, userId = user.Id }, httpRequest.Scheme);

            string subject = string.Empty;
            string message = string.Empty;

            switch (emailType)
            {
                case EmailType.Register:
                    {
                        subject = "Confirmation Your Account in School Management System  APP";
                        message = $"<h1>Confirm Your Account By Clicking <a href='{Url}'>Here</a></h1>";
                    }
                    break;
                case EmailType.ForgetPassword:
                    {
                        var Opt = new Random().Next(1000, 9999).ToString();
                        await _unitOfWork.UserOtp.AddAsync(new UserOTP
                        {
                            UserID= user.Id,
                            OTP=Opt,
                        });
                        _unitOfWork.SaveChange();
                        subject = " Reset Password  Your Account in School Management System  APP";
                        message = $"<h1>Number Of Verify : {Opt}</h1>";

                    }
                    break;
            }

            await _emailSender.SendEmailAsync(user.Email!, subject, message);


        }
    }
}
