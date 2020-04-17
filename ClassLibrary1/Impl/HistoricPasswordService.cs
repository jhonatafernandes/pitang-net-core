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
    public class HistoricPasswordService : IHistoricPasswordService
    {

        public async Task<List<HistoricPassword>> GetAllHistoricPassword(
            DataContext context)
        {
            var HistoricPasswords = await context.Passwords.AsNoTracking().ToListAsync();
            return HistoricPasswords;

        }

        public async Task<HistoricPassword> GetHistoricPassword(
            DataContext context,
            int id
            )
        {
            var HistoricPassword = await context.Passwords.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return HistoricPassword;
        }


        public async Task<HistoricPassword> PostHistoricPassword(
            DataContext context,
            HistoricPassword model
            )
        {
            try
            {
                context.Passwords.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch
            {
                return null;
            }
        }

        public async Task<dynamic> PutHistoricPassword(
            DataContext context,
            HistoricPassword model)
        {
            try
            {
                context.Entry<HistoricPassword>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;

            }
            catch
            {
                return null;
            }
        }

        public async Task<dynamic> DeleteHistoricPassword(
            DataContext context,
            HistoricPassword model)
        {
            try
            {
                context.Passwords.Remove(model);
                await context.SaveChangesAsync();
                return new { okMessage = "HistoricPassword deletado com sucesso!" };
            }
            catch
            {
                return new { badMessage = "Não foi possível excluir o HistoricPassword." };
            }
        }



    }
}
