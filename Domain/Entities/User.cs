using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Domain.Common;

namespace SocialNetwork.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Gender { get; set; }
        public bool IsReported { get; set; }
        public bool IsBlocked { get; set; }

    }
}
