using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVBank.Models
{
    public class FileUploadModel
    {
        public SelectList Pipelines { get; set; }
        public SingleFileUpload FileUpload { get; set; }
    }
}
