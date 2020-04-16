using Microsoft.EntityFrameworkCore;
using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.Entities.Models;
using Pitang.Sms.NetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pitang.Sms.NetCore.Services
{
    public class UserService :  IUserService
    {

        public async Task<List<User>> GetAllUsers(
            DataContext context)
        {
            var users = await context.Users.AsNoTracking().ToListAsync();
            return users;

        }

        public async Task<User> GetUser(
            DataContext context,
            int id
            )
        {
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<dynamic> PostLoginUser(
            DataContext context,
            User model)
        {
            var user = await context.Users
                    .AsNoTracking()
                    .Where(x => x.Username == model.Username && x.Password == model.Password)
                    .FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> PostUser(
            DataContext context,
            User model
            )
        {
            try
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch
            {
                return null;
            }
        }

        public async Task<dynamic> PutUser(
            DataContext context,
            User model)
        {
            try
            {
                context.Entry<User>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;

            }
            catch
            {
                return null;
            }
        }







        public async Task<dynamic> DeleteUser(
            DataContext context,
            User model)
        {
            try
            {
                context.Users.Remove(model);
                await context.SaveChangesAsync();
                return new { okMessage = "Usuário deletado com sucesso!" };
            }
            catch
            {
                return new { badMessage = "Não foi possível excluir o usuário." };
            }
        }


        
    }
}
