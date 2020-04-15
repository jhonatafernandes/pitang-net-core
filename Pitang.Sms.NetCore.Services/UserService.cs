using Microsoft.AspNetCore.Mvc;
using Pitang.Sms.NetCore.Data.DataContext;
using System.Threading.Tasks;
using Pitang.Sms.NetCore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Pitang.Sms.NetCore.Services
{
    public class UserService
    {
        public static async Task<List<User>> GetAllUsers(
           DataContext context)
        {
            
            var users = await context.Users.AsNoTracking().ToListAsync();
            return users;
        }

        public static async Task<User> GetUser(
           DataContext context,
           int id)
        {
            Console.WriteLine("TO NO SERVICE de novo KK");
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

    }
}
