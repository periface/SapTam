using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace Sap.ToDoLists.Entities
{
    public  class Todo : FullAuditedEntity
    {
        public virtual ToDoList ToDoList { get; set; }
    }
}
