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
    public class StorieService : IStorieService
    {

        public async Task<List<Storie>> GetAllStories(
            DataContext context)
        {
            var Stories = await context.Stories.AsNoTracking().ToListAsync();
            return Stories;

        }

        public async Task<Storie> GetStorie(
            DataContext context,
            int id
            )
        {
            var Storie = await context.Stories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Storie;
        }


        public async Task<dynamic> PostStorie(
            DataContext context,
            Storie model
            )
        {
            try
            {
                context.Stories.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch
            {
                return "Não foi possível adicionar o storie";
            }
        }

        public async Task<dynamic> PutStorie(
            DataContext context,
            Storie model)
        {
            try
            {
                context.Entry<Storie>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;

            }
            catch
            {
                return null;
            }
        }

        public async Task<dynamic> DeleteStorie(
            DataContext context,
            Storie model)
        {
            try
            {
                context.Stories.Remove(model);
                await context.SaveChangesAsync();
                return new { okMessage = "Storie deletado com sucesso!" };
            }
            catch
            {
                return new { badMessage = "Não foi possível excluir o storie." };
            }
        }



    }
}
