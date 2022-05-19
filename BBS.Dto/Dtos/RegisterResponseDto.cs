using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Dto.Dtos
{
    public class RegisterResponseDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public bool Succeeded { get; set; }
        public SignInResult Errors { get; }
    }
}
