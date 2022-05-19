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

namespace BusBookingSystem.Controllers
{
    public class BookerDashBoardController : Controller
    {
        private readonly IUserService _userService;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        public BookerDashBoardController(IUserService userService,
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
        }
        public IActionResult Index()
        {
            return View();
        }
        [NonAction]
        public string SetTitle()
        {
            return (this.User.Identity.IsAuthenticated) ? this.User.Identity.Name : "Buss Booking Application";
        }

        public async Task<IActionResult> Users()
        {
            ViewData["Title"] = SetTitle();
            var model = await _userService.GetUsers();
            return View(model);
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
                    return RedirectToAction("Users", "BookerDashboard");
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
                    return RedirectToAction("Users", "BookerDashboard");
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
            return RedirectToAction("Users", "BookerDashboard");


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
        
    }
}
