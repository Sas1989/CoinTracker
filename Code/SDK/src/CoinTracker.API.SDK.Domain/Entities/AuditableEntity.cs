using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SDK.Domain.Entities
{
    public abstract class AuditableEntity : Entity
    {
        public DateTime CreationDate;
        public DateTime UpdateDate;

        protected AuditableEntity()
        {
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }

        protected void ChangeUpdateTime()
        {
            UpdateDate = DateTime.Now;
        }

    }
}
