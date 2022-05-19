using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Dto.Dtos
{
    public class ResponseSaveFileDto
    {
        public long UserFileId { get; set; }
        public string FilePath { get; set; }
        public string RunId { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
