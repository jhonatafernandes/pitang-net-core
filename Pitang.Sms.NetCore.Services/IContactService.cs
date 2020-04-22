using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Pitang.Sms.NetCore.Services
{
    public interface IContactService
    {
        public Task<List<Contact>> GetAllContacts(
            DataContext context);

        public Task<Contact> GetContact(
            DataContext context,
            int id
            );

        public Task<dynamic> PostContact(
            DataContext context,
            Contact model
            );

        public Task<dynamic> PutContact(
            DataContext context,
            Contact model
           );

        public Task<dynamic> DeleteContact(
            DataContext context,
            Contact model
            );
    }
}
