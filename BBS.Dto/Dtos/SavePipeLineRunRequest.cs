using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Dto.Dtos
{
    public class SavePipeLineRunRequest
    {
        public string RunId { get; set; }
        public long  UserFileId { get; set; }
        public string PipelineName { get; set; }
    }
}
