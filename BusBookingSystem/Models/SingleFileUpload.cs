using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVBank.Models
{
    public class SingleFileUpload
    {
        [Required(ErrorMessage = "Please Select a File")]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
        [Required(ErrorMessage ="Please Select a Pipeline")]
        [Display(Name = "Pipeline")]
        public string Pipeline { get; set; }
    }
}
