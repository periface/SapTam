using Abp.Domain.Entities.Auditing;

namespace SapModule.Core.ProjectMembers.Entities
{
    public class ProjectMember : FullAuditedEntity
    {
        protected ProjectMember()
        {

        }
        public long UserId { get; protected set; }
        public int ProjectId { get; protected set; }
        public bool Leader { get; protected set; }

        public static ProjectMember CreateMember(long userId,int projectId,bool isLeader)
        {
            return new ProjectMember()
            {
                UserId = userId,
                ProjectId = projectId,
                Leader = isLeader
            };
        }
        public void MakeLeader()
        {
            Leader = true;
        }

        public void UnMakeLeader()
        {
            Leader = false;
        }
    }
}
