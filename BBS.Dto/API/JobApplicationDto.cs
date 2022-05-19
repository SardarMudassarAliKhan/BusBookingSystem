using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Dto.API
{
    public class JobApplicationDto
    {
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(20, ErrorMessage = "Name length can't be more than 20.")]
        [RegularExpression("[a-zA-Z ]+", ErrorMessage = "only alphabet")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is Required")]
        [RegularExpression("^[0,9][1-9]\\d{10}$|^[1-9]\\d{10}$", ErrorMessage = "Phone Number is not valid")]
        public string Phone { get; set; }
        public string CvUrl { get; set; }
        [Required(ErrorMessage = "Years of Experience is Required")]
        [RegularExpression("^100|[1-9]?\\d$", ErrorMessage = "Experience is not valid")]
        public int YearsOfExperience { get; set; }
        [Required(ErrorMessage = "Area is Required")]
        public string Area { get; set; }
        [Required(ErrorMessage = "Skills is Required")]
        public string Skills { get; set; }
        [Required(ErrorMessage = "Current Salary is Required")]
        public decimal CurrentSalary { get; set; }
        [Required(ErrorMessage = "Expected Salary is Required")]
        public decimal ExpectedSalary { get; set; }
        [Required(ErrorMessage = "Current Organization is Required")]
        public string CurrentOrganization { get; set; }
        [Required(ErrorMessage = "Current Designation is Required")]
        public string CurrentDesignation { get; set; }
        [Required(ErrorMessage = "Last University is Required")]
        public string LastUniversity { get; set; }
        [Required(ErrorMessage = "Last Degree is Required")]
        public string LastDegree { get; set; }
        [Required(ErrorMessage = "Location is Required")]
        public string Location { get; set; }
    }
}
