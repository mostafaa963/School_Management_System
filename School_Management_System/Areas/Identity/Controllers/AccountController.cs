using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Services.AccountServices;
using School_Management_System.Utilities;

namespace School_Management_System.Areas.Identity.Controllers
{
    [Area(SD.ADENTITY_AREA)]
    public class AccountController : Controller

    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountServices _accountServices;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager
            , IAccountServices accountServices)
        {
            _accountServices = accountServices;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        [HttpGet]
        public async Task<IActionResult> LogIn()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginVM userLogin)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(userLogin.EmailOrUserName, "User Name or Email is Not Valid");
                ModelState.AddModelError(userLogin.Pssword, "Password is Not Valid");
                return View(userLogin);
            }
            var userNameOrEmail = await _userManager.FindByEmailAsync(userLogin.EmailOrUserName) ??
                await _userManager.FindByNameAsync(userLogin.EmailOrUserName);
            if (userNameOrEmail == null)
                return NotFound();
            var checkPassword = await _userManager.CheckPasswordAsync(userNameOrEmail, userLogin.Pssword);

            var signIn = await _signInManager.PasswordSignInAsync(userNameOrEmail, userLogin.Pssword, userLogin.IsPersistent, true);
            if (!signIn.Succeeded)
            {
                ModelState.AddModelError(userLogin.EmailOrUserName, "User Name or Email is Not Valid");
                ModelState.AddModelError(userLogin.Pssword, "Password is Not Valid");
                return View(userLogin);
            }

            return RedirectToAction("Index", "Home", new { area = "Student" });
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM userRegister)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegister);
            }

            var user = new User()
            {
                FullName = $"{userRegister.FName} {userRegister.LName}",
                UserName = userRegister.UserName,
                Email = userRegister.Email,
            };
            var Result = await _userManager.CreateAsync(user, userRegister.Password);
            if (!Result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, string.Join(" , ", Result.Errors.Select(e => e.Description)));
                return View(userRegister);
            }
            //generate token to send to  User for Confirm Email
            await _accountServices.SendMailAsync(user, Url, Request);
            var resultRole = await _userManager.AddToRoleAsync(user, RolesNames.STUDENT_ROLES);

            if (!resultRole.Succeeded)
            {
                ModelState.AddModelError(string.Empty, string.Join(" , ", resultRole.Errors.Select(e => e.Description)));
                return View(userRegister);
            }


            TempData["success_notification"] = "Add Account Successfully, check you email";

            return RedirectToAction(nameof(LogIn));
        }
        public async Task<IActionResult> Confirm(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return NotFound();

            var confirmEmail = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmEmail.Succeeded)
                TempData["error_notification"] = String.Join(",", confirmEmail.Errors.Select(e => e.Description));

            TempData["success_notification"] = "Confirm Email Successfully, You can login now";
            return RedirectToAction(nameof(LogIn));

        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM? userChangePassword)
        {
            if (!ModelState.IsValid || userChangePassword is null)
                return View(userChangePassword);

            var user =await _userManager.FindByEmailAsync(userChangePassword.EmailOrUserName!) ?? 
                await _userManager.FindByNameAsync(userChangePassword.EmailOrUserName!);
            if (user is null)
                return NotFound();
          var result=  await _userManager.ChangePasswordAsync(user, userChangePassword.OldPassword!, userChangePassword.NewPassword!);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty,string.Join(",",result.Errors.Select(e => e.Description)));
            }
            TempData["success_notification"] = " Password Change Successfully.. ";
            return RedirectToAction(nameof(LogIn));
        }

    }
}
