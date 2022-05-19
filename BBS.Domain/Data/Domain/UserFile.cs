using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Domain.Data.Domain
{
    public class UserFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserFileId { get; set; }
        public string FilePath { get; set; }
        public string FileDirectory { get; set; }
        public string RunId { get; set; }
        public string Status { get; set; }
        public string PipelineTag { get; set; }
        public string PipelineName { get; set; }
        public string FileName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("ApplicationUser")]
        public long UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
