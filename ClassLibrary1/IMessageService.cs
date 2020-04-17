using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pitang.Sms.NetCore.Services
{
    public interface IMessageService
    {
        public Task<List<Messages>> GetAllMessages(
            DataContext context);

        public Task<Messages> GetMessage(
            DataContext context,
            int id
            );

        public Task<Messages> PostMessage(
            DataContext context,
            Messages model
            );

   

        public Task<dynamic> PutMessage(
            DataContext context,
            Messages model
           );

        public Task<dynamic> DeleteMessage(
            DataContext context,
            Messages model
            );
    }
}
