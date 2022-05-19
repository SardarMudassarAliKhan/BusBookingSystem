using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Dto.Dtos
{
    public class JobApplicationResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CvUrl { get; set; }
        public int YearsOfExperience { get; set; }
        public string Area { get; set; }
        public string Skills { get; set; }
        public decimal CurrentSalary { get; set; }
        public decimal ExpectedSalary { get; set; }
        public string CurrentOrganization { get; set; }
        public string CurrentDesignation { get; set; }
        public string LastUniversity { get; set; }
        public string LastDegree { get; set; }
        public string Location { get; set; }
    }
}
