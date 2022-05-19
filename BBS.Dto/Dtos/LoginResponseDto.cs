using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Dto.Dtos
{
    public class LoginResponseDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public bool Succeeded { get; set; }
    }
}
