using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Dto.Dtos
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your password.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter your Phone Number.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please enter your password.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password does not match")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "I agree to the Terms & Conditions")]
        [Required]
        [Compare("isTrue", ErrorMessage = "Please agree to Terms and Conditions")]
        public bool TermConditons { get; set; }
        public bool isTrue
        { get { return true; } }

        public string RoleName { get; set; } = "Admin";
    }
}
