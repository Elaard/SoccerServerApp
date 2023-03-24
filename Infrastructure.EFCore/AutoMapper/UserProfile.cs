using AutoMapper;
using Common.ModelsDTO;
using Infrastructure.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EFCore.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
