using System;
using Abp.Domain.Entities.Auditing;

namespace SapModule.Core.LaboralDaysManager.Entities
{
    public class NoLaboralDay : FullAuditedEntity
    {
        public DateTime Day { get; set; }
    }
}
