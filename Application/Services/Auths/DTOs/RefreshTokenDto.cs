using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services.Auths.DTOs
{
    public sealed class RefreshTokenDto
    {
        public string Token { get; }
        public RefreshTokenDto(string token) 
        {
            Token = token;
        }
    }
}
