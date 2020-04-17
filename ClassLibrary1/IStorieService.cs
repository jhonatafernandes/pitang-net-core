using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pitang.Sms.NetCore.Services
{
    public interface IStorieService
    {
        public Task<List<Storie>> GetAllStories(
            DataContext context);

        public Task<Storie> GetStorie(
            DataContext context,
            int id
            );

        public Task<Storie> PostStorie(
            DataContext context,
            Storie model
            );

        public Task<dynamic> PutStorie(
            DataContext context,
            Storie model
           );

        public Task<dynamic> DeleteStorie(
            DataContext context,
            Storie model
            );
    }
}
