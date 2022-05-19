using Microsoft.AspNetCore.Mvc.Rendering;
using CVBank.Dto.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVBank.Models
{
    public class DownloadViewModel
    {
        public DownloadViewModel()
        {
            UserFiles = new List<FileDto>();
        }
        public List<FileDto> UserFiles { get; set; }
        public SelectList Pipelines { get; set; }

    }
}
