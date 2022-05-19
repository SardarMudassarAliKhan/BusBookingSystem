using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVBank.Models
{
    public class SingleFileUploadResponse
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }

    }
}
