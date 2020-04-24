using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.DTO.User;
using Pitang.Sms.NetCore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pitang.Sms.NetCore.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        protected readonly DataContext _context;
        public UserRepository([FromServices] DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(
            int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<List<User>> Get()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return users;
        }

        public User Post(
            User model)
        {
             _context.Users.Add(model);
            return model;
        }

        public User Put(User model)
        {
            _context.Entry<User>(model).State = EntityState.Modified;

            return model;
        }
        public async Task<User> Authenticate(LoginUserDto model)
        {
            var user = await _context.Users
                    .AsNoTracking()
                    .Where(x => x.Email == model.Email)
                    .FirstOrDefaultAsync();

            return user;
        }

        public void Delete(User model)
        {
            _context.Users.Remove(model);
            
        }

        public void HistoricPassword(
            User model)
        {
            _context.Passwords.Add(new HistoricPassword { User = model, Password = model.Password });
        }
    }
}
