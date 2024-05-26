using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services.Auths.DTOs
{
    public sealed class RegisterRequest
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }
        public string Email { get; }
        public string Phone {  get; }
        public string Password { get; }
        public string PasswordConfirm { get; }

        public RegisterRequest(string firstName, string lastName, string middleName, string email, string phone, string password, string passwordConfirm)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            MiddleName = middleName;
            Password = password;
            PasswordConfirm = passwordConfirm;
        }
    }
}
