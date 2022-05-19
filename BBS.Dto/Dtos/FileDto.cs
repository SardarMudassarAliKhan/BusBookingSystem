using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Dto.Dtos
{
    public class FileDto
    {
        public long UserFileId { get; set; }
        public string FilePath { get; set; }
        public string PipelineTag { get; set; }
        public string PipelineName { get; set; }
        public string FileName { get; set; }
        public string FileDirectory { get; set; }
        public string Status { get; set; }
        public string RunId { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UserId { get; set; }
    }
}
