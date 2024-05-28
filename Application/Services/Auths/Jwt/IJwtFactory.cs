using SocialNetwork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services.Auths.Jwt
{
    public interface IJwtFactory
    {
        string GenerateToken(User user, int expireDay);
        IDictionary<string, object> VerifyToken(string token);
    }
}
