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
    public class ContactService :  IContactService
    {

        public async Task<List<Contact>> GetAllContacts(
            DataContext context)
        {
            var Contacts = await context.Contacts.AsNoTracking().ToListAsync();
            return Contacts;

        }

        public async Task<Contact> GetContact(
            DataContext context,
            int id
            )
        {
            var Contact = await context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Contact;
        }


        public async Task<Contact> PostContact(
            DataContext context,
            Contact model
            )
        {
            try
            {
                context.Contacts.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch
            {
                return null;
            }
        }

        public async Task<dynamic> PutContact(
            DataContext context,
            Contact model)
        {
            try
            {
                context.Entry<Contact>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;

            }
            catch
            {
                return null;
            }
        }

        public async Task<dynamic> DeleteContact(
            DataContext context,
            Contact model)
        {
            try
            {
                context.Contacts.Remove(model);
                await context.SaveChangesAsync();
                return new { okMessage = "Contato deletado com sucesso!" };
            }
            catch
            {
                return new { badMessage = "Não foi possível excluir o contato." };
            }
        } 
    }
}
