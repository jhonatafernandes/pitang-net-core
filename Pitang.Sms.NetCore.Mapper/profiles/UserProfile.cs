using AutoMapper;
using Pitang.Sms.NetCore.Entities.Models;
using Pitang.Sms.NetCore.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pitang.Sms.NetCore.Mapper.profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<GetUserDto, User>();
            /*etc...*/
        }
    }
}
