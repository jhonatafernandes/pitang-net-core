using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.Entities.auxiliares;
using Pitang.Sms.NetCore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pitang.Sms.NetCore.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers(
            DataContext context);

        public Task<User> GetUser(
            DataContext context,
            int id
            );

        public Task<dynamic> PostUser(
            DataContext context,
            User model
            );

        public Task<User> PostLoginUser(
            DataContext context,
            UserLogin model
            );

        public Task<dynamic> PutUser(
            DataContext context,
            User model
           );

        public Task<dynamic> DeleteUser(
            DataContext context,
            User model
            );




    }
}
