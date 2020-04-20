using Microsoft.EntityFrameworkCore;
using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.Entities.auxiliares;
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

            //Precisa-se encontrar a maneira mais correta de extrair do banco sem a senha
            //Foreach abaixo como uma solução temporária
            foreach(User user in users)
            {
                user.Password = "";
            }
            return users;

        }

        public async Task<User> GetUser(
            DataContext context,
            int id
            )
        {
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user is User)
            {
                user.Password = "";
            }
            return user;
        }

        public async Task<User> PostLoginUser(
            DataContext context,
            UserLogin model)
        {
            var user = await context.Users
                    .AsNoTracking()
                    .Where(x => x.Email == model.Email)
                    .FirstOrDefaultAsync();
            if (user is User)
            {
                return user;
            }
            return null;
        }

        public async Task<dynamic> PostUser(
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
            catch (DbUpdateException)
            {
                return "Username ou email já existem!";
            }
            catch
            {
                return "não foi possível criar um usuário, tente novamente!";
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
            catch (DbUpdateException)
            {
                return "Username ou Email já existem!";
            }
            catch
            {
                return "Não foi possível alterar o usuário.";
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
                return model;
            }
            catch
            {
                return new { badMessage = "Não foi possível excluir o usuário." };
            }
        }


        
    }
}
