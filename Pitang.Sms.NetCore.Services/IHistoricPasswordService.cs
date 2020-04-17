using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pitang.Sms.NetCore.Services
{
    public interface IHistoricPasswordService
    {
        public Task<List<HistoricPassword>> GetAllHistoricPassword(
           DataContext context);

        public Task<HistoricPassword> GetHistoricPassword(
            DataContext context,
            int id
            );

        public Task<HistoricPassword> PostHistoricPassword(
            DataContext context,
            HistoricPassword model
            );

        public Task<dynamic> PutHistoricPassword(
            DataContext context,
            HistoricPassword model
           );

        public Task<dynamic> DeleteHistoricPassword(
            DataContext context,
            HistoricPassword model
            );
    }
}
