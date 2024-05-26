using AutoMapper;
using SocialNetwork.Application.Services.Auths.DTOs;
using SocialNetwork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Utils
{
    public class AutoMapperProfile : Profile
    {   
        public AutoMapperProfile() 
        {
            CreateMap<RegisterRequest, User>();
            CreateMap<User, RegisterResponse>();

            CreateMap<User, LoginResponse>();
        }
    }
}
