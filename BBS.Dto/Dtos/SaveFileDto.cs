using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Dto.Dtos
{
    public class SaveFileDto
    {
        public string FilePath { get; set; }
        public string TempFileName { get; set; }
        public string FileName { get; set; }
        public long UserId { get; set; }
        public string PipelineTag { get; set; }

    }
}
