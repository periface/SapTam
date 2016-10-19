using Abp.Domain.Entities.Auditing;

namespace SapModule.Core.ToDo.Entities
{
    public class HistoryStatus : FullAuditedEntity
    {
        public virtual Todo Todo { get; set; }
        public int StatusType { get; set; }
        //Si quiere
        public string Observations { get; set; }
    }
}
