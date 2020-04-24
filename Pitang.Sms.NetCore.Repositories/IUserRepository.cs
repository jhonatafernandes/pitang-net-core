using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.DTO.User;
using Pitang.Sms.NetCore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pitang.Sms.NetCore.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> Authenticate(LoginUserDto model);

        public void HistoricPassword(User model);

    }
}
