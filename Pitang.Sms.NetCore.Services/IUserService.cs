using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.DTO.User;
using Pitang.Sms.NetCore.Entities.Models;
using Pitang.Sms.NetCore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pitang.Sms.NetCore.Services
{
    public interface IUserService
    {
        public Task<List<GetUserDto>> Get();

        public Task<GetUserDto> GetById(
            int id
            );

        public dynamic Post(
            User model
            );

        public Task<User> Authenticate(
            LoginUserDto model
            );

        public Task<dynamic> Put(
            int id,
            GetUserDto model
           );

        public Task<dynamic> Delete(
            int id
            );




    }
}
