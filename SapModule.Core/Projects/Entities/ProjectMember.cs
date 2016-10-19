using Abp.Domain.Entities.Auditing;

namespace Sap.Projects.Entities
{
    public class ProjectMember : FullAuditedEntity
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public bool Leader { get; set; }
    }
}
