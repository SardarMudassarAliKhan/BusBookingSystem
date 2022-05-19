using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Domain.Data.Domain
{
    public class ApplicationUser : IdentityUser<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NicNo { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public ICollection<UserFile> UserFiles { get; set;}
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
