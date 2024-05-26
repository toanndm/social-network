using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services.Auths.DTOs
{
    public sealed class RegisterResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
