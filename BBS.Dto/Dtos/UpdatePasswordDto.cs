using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Dto.Dtos
{
    public class UpdatePasswordDto
    {
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your Old Password.")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Please enter your New Password.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Invalid Password")]

        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please enter your Confirm Password.")]
        [Compare("NewPassword", ErrorMessage = "Password and Confirm Password does not match")]

        public string ConfirmPassword { get; set; }
    }
}
