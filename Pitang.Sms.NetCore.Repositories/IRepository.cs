using Pitang.Sms.NetCore.Entities;
using Pitang.Sms.NetCore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pitang.Sms.NetCore.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<T> GetById(int id);
        public Task<List<T>> Get();
        public T Post(T entity);
        public T Put(T entity);
        public void Delete(T entity);
    }
}
