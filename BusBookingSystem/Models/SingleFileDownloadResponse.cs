using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVBank.Models
{
    public class SingleFileDownloadResponse
    {
        public string FileName { get; set; }
        public byte[] bytes { get; set; }
    }
}
