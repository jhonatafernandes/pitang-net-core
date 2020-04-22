using Microsoft.EntityFrameworkCore;
using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.Entities.Models;
using Pitang.Sms.NetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pitang.Sms.NetCore.Services.Impl
{
    public class MessageService : IMessageService
    {

        public async Task<List<Messages>> GetAllMessages(
            DataContext context)
        {
            var Messages = await context.Messages.AsNoTracking().ToListAsync();
            return Messages;

        }

        public async Task<Messages> GetMessage(
            DataContext context,
            int id
            )
        {
            var Message = await context.Messages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Message;
        }


        public async Task<Messages> PostMessage(
            DataContext context,
            Messages model
            )
        {
            //var validateContact = await context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.OwnerId)
            try
            {
                context.Messages.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch
            {
                return null;
            }
        }

        public async Task<dynamic> PutMessage(
            DataContext context,
            Messages model)
        {
            try
            {
                context.Entry<Messages>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;

            }
            catch
            {
                return null;
            }
        }

        public async Task<dynamic> DeleteMessage(
            DataContext context,
            Messages model)
        {
            try
            {
                context.Messages.Remove(model);
                await context.SaveChangesAsync();
                return new { okMessage = "Message deletado com sucesso!" };
            }
            catch
            {
                return new { badMessage = "Não foi possível excluir o Message." };
            }
        }



    }
}
