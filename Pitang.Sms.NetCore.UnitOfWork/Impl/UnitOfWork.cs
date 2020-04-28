using Microsoft.AspNetCore.Mvc;
using Pitang.Sms.NetCore.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pitang.Sms.NetCore.UnitOfWork.Impl
{
    public class UnitOfWork : IUnitOfWork

    {
        protected readonly DataContext _context;
        public UnitOfWork([FromServices] DataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            //
        }
    }
}
