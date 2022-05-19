using Microsoft.AspNetCore.Identity;
using CVBank.Dto.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVBank.Domain.Data.Domain;

namespace CVBank.InfraStructure.Service.User
{
    public interface IUserService
    {
        Task<LoginResponseDto> Login(LoginRequestDto request);
        Task<IdentityResult> CreateUser(UserDto request);
        Task<IdentityResult> Register(RegisterRequestDto request);
        Task<UserDto> GetUserByName(string name);
        Task<IdentityResult> UpdateUser(UserDto request);
        Task<IdentityResult> DeleteUser(string name);
        Task<IdentityResult> UpdatePassword(UpdatePasswordDto request);
        Task<IdentityResult> ResetPassword(ResetPasswordViewModel request);
        Task<string> SendPasswordResetLink(string email);
        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task<bool> Logout();
    }
}
