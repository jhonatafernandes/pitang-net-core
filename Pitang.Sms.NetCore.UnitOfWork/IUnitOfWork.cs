using System;
using System.Collections.Generic;
using System.Text;

namespace Pitang.Sms.NetCore.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}
