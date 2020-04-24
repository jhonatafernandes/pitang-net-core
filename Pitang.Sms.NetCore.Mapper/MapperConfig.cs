using AutoMapper;
using Pitang.Sms.NetCore.DTO.User;
using Pitang.Sms.NetCore.Entities.Models;
using Pitang.Sms.NetCore.Mapper.profiles;
using System;

namespace Pitang.Sms.NetCore.Mapper
{
    public class MapperConfig
    {
        private readonly MapperConfiguration config;
        public IMapper iMapper;
        public MapperConfig()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GetUserDto, User>();
                cfg.CreateMap<User, GetUserDto>();
            });

            iMapper = config.CreateMapper();
        }
    }
}
