using Common.Helper;
using Common.Helper.EmailTemplate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CVBank.Domain.Data.Domain;
using CVBank.Dto.Dtos;
using CVBank.InfraStructure.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CVBank.Controllers 
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        public AccountController(IUserService userService,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment env,
            RoleManager<ApplicationRole> roleManager)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
            _env = env;
            _roleManager = roleManager; 
            /*Testing*/
        }
        public IActionResult Index()
        {
            DateTime myDate = new DateTime(1624358824975);
            String test = myDate.ToString("MMMM dd, yyyy");
            return View();
        }
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return View();
        }
        [HttpPost]
        [ActionName("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            if (ModelState.IsValid)
            {
                string token = await _userService.SendPasswordResetLink(model.Email);
                if (!string.IsNullOrEmpty(token))
                {
                    var userName = "User";
                    var user = await _userService.GetUserByName(model.Email);
                    if (!(user is null) && !string.IsNullOrEmpty(user.FirstName))
                    {
                        userName = user.FirstName + " " + user.LastName;
                    }

                    var resetLink = Url.Action("ResetPassword",
                                                "Account", new { token = token },
                                                 protocol: HttpContext.Request.Scheme);
                    var _template = EmailTemplate.GetTemplate(_env.ContentRootPath + "\\wwwroot\\EmailTemplates\\ResetPassword.txt");
                    _template = _template.Replace("{user_name}", userName).Replace("{reset_link}", resetLink);
                    //string _template = $"Please Click on the Link to <a href='{resetLink}'>Reset Password</a>";
                    var request = new MailRequest()
                    {
                        ToEmail = model.Email,
                        Subject = "Reset Password",
                        Body = _template
                    };
                    await EmailService.SendEmailAsync(request);
                    // code to email the above link
                    // see the earlier article

                    TempData["ForgotSuccessMessage"] = $"Password reset link has been sent to your email address!";
                    return RedirectToAction("Login", "Account");
                }
            }
            ViewData["Title"] = "Forgot Password";
            TempData["ErrorMessage"] = $"Invalid Email Address";
            return View("ForgotPassword", model);

        }
        [ActionName("forgot-password")]
        public IActionResult ForgotPassword()
        {
            ViewData["Title"] = "Forgot Password";
            var model = new ForgotPasswordRequest();
            return View("ForgotPassword", model);
        }
        public IActionResult ResetPassword(string token)
        {
            var model = new ResetPasswordViewModel()
            {
                Token = token
            };
            ViewData["Title"] = "Reset Password";
            return View("ResetPassword", model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword
                (ResetPasswordViewModel obj)
        {
            IdentityResult result = await _userService.ResetPassword(obj);
            if (!(result is null) && result.Succeeded)
            {
                TempData["ResetSuccessMessage"] = "Password reset successful!";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (result is null) TempData["ErrorMessage"] = "Invalid User Email!";
                else TempData["ErrorMessage"] = result.Errors.FirstOrDefault().Description;
                return View();
            }
        }
        [ActionName("verify-email")]
        public IActionResult VerifyEmail()
        {
            ViewData["Title"] = "Verify Email";
            return View("VerifyEmail");
        }

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {

            if (ModelState.IsValid)
            {
                var result = await _userService.Login(request);
                if (result.Succeeded)
                    if(this.User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    if(this.User.IsInRole("Booker"))
                    {
                        return RedirectToAction("Index", "BookerDashBoard");
                    }
                    if (this.User.IsInRole("Driver"))
                    {
                        return RedirectToAction("Index", "DriverDashBoard");
                    }
                    if (this.User.IsInRole("Operations"))
                    {
                        return RedirectToAction("Index", "OperationDashboard");
                    }
                    if (this.User.IsInRole("Finance"))
                    {
                        return RedirectToAction("Index", "FinanceDashboard");
                    }
            }
            ViewData["Title"] = "Login";
            ModelState.AddModelError("", "Email Or Password is Invalid");
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Login", "Account");
        }
        public IActionResult Register()
        {
            ViewData["Title"] = "Register";
            return View();
        }

        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Register(request);
                if (result.Succeeded)
                {
                    TempData["RegisterSuccessMessage"] = "User Registered successfully!";
                    return RedirectToAction("Login", "Account");
                }
                ViewData["Title"] = "Register";
                foreach (var error in result.Errors)
                {
                    if (!error.Description.StartsWith("Username"))
                        ModelState.AddModelError("", error.Description);
                }

            }
            return View();
        }

        public IActionResult CreateUser()
        {
            ViewData["Title"] = "Create User";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateUser(user);
                if (result.Succeeded)
                {
                    TempData["UserMessage"] = "User Created successfully!";
                    return RedirectToAction("Users", "Dashboard");
                }
                ViewData["Title"] = "Create User";
                foreach (var error in result.Errors)
                {
                    if (!error.Description.StartsWith("Username"))
                        ModelState.AddModelError("", error.Description);
                }

            }
            return View();
        }

        public async Task<IActionResult> EditUser(string name)
        {
            var user = await _userService.GetUserByName(name);

            ViewData["Title"] = "Edit User";

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserDto user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUser(user);
                if (result.Succeeded)
                {
                    TempData["UserMessage"] = "User Updated successfully!";
                    return RedirectToAction("Users", "Dashboard");
                }
                ViewData["Title"] = "Edit User";

                foreach (var error in result.Errors)
                {
                    if (!error.Description.StartsWith("Username"))
                        ModelState.AddModelError("", error.Description);
                }


            }
            return View(user);
        }


        public async Task<IActionResult> Delete(string name)
        {
            var response = await _userService.DeleteUser(name);

            if (response.Succeeded)
            {
                TempData["UserMessage"] = "User deleted successfully!";
            }
            return RedirectToAction("Users", "Dashboard");


        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            if (result.Succeeded)
                return RedirectToAction("Index", "Dashboard");
            else
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };

                IdentityResult identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Search", "Dashboard");
                    }
                }
                return AccessDenied();
            }
        }
    }
}
