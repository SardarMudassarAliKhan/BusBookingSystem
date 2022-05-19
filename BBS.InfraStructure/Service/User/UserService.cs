using Microsoft.AspNetCore.Identity;
using CVBank.Domain.Data.Domain;
using CVBank.Dto.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CVBank.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBank.InfraStructure.Service.User
{
    public class UserService : IUserService
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public UserService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<UserDto> GetUserByName(string name)
        {
            var response = new UserDto();
            var user = await _userManager.FindByNameAsync(name);
            var roles = await _userManager.GetRolesAsync(user);
            if (user is null) return response;
            response.Id = user.Id;
            response.Email = user.Email;
            response.PhoneNumber = user.PhoneNumber;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.UserName = user.UserName;
            response.RoleName = roles?.FirstOrDefault();
            return response;
        }


        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            var response = new LoginResponseDto()
            {

            };
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                var claim = new Claim(ClaimTypes.Role, "Test");
                await _userManager.AddClaimAsync(user, claim);
                response.Id = user.Id;
                response.Username = user.UserName;
                response.Succeeded = true;
            }
            return response;
        }

        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<IdentityResult> Register(RegisterRequestDto request)
        {
            var response = new RegisterResponseDto()
            {

            };
            ApplicationUser user = new ApplicationUser
            {

                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            try
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync(request.RoleName);
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new ApplicationRole(request.RoleName));
                    }

                    if (!await _userManager.IsInRoleAsync(user, request.RoleName))
                    {
                        await _userManager.AddToRoleAsync(user, request.RoleName);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        public async Task<IdentityResult> DeleteUser(string name)
        {
            IdentityResult response = new IdentityResult();

            var user = await _userManager.FindByNameAsync(name);
         
            if (user != null)
            {
                 response = await _userManager.DeleteAsync(user);
               
            }
            return response;
        }


        public async Task<IdentityResult> CreateUser(UserDto request)
        {

            ApplicationUser AppUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            try
            {
                var result = await _userManager.CreateAsync(AppUser, request.Password);
                if (result.Succeeded)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync(request.RoleName);
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new ApplicationRole(request.RoleName));
                    }

                    if (!await _userManager.IsInRoleAsync(AppUser, request.RoleName))
                    {
                        await _userManager.AddToRoleAsync(AppUser, request.RoleName);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IdentityResult> UpdatePassword(UpdatePasswordDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            IdentityResult result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            return result;
        }
        public async Task<IdentityResult> UpdateUser(UserDto request)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                if (!await _userManager.IsInRoleAsync(user, request.RoleName))
                {
                    await _userManager.AddToRoleAsync(user, request.RoleName);
                }
            }
            return result;
        }
        public async Task<string> SendPasswordResetLink(string email)
        {
            string token = string.Empty;
            ApplicationUser user = await _userManager.FindByNameAsync(email);

            //if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            //{

            //}
            if (!(user is null))
            {
                token = await _userManager.GeneratePasswordResetTokenAsync(user);
            }
            return token;


        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordViewModel request)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(request.Email);
            if (user is null)
            {
                return null;
            }

            IdentityResult result = _userManager.ResetPasswordAsync(user, request.Token, request.Password).Result;
            return result;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            IEnumerable<ApplicationUser> users = await _context.Users.ToListAsync();

            return users;
        }
    }
}
