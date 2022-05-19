using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Dto.Dtos
{
    public class UserDto
    {
        [Required]
        public long Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter First Name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter Last Name.")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter Phone Number.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter your CNIC/Passport No")]
        public string NicNo { get; set; }
        [Required(ErrorMessage = "Please enter your Nationality")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Please enter your Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string RoleName { get; set; }
        public string UserName { get; set; }
    }
}
