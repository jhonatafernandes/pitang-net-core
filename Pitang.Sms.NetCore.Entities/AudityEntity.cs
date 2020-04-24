using System;
using System.Collections.Generic;
using System.Text;

namespace Pitang.Sms.NetCore.Entities
{
    public abstract class AuditEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
