using Abp.Domain.Entities.Auditing;
using SapModule.Core.Projects.Entities;
using System.Collections.Generic;

namespace SapModule.Core.Client.Entities
{
    public class ClientCompany : FullAuditedEntity
    {
        protected ClientCompany()
        {

        }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public virtual ICollection<ClientInfo> Clients { get; protected set; }
        public virtual ICollection<Project> Projects { get; protected set; }

        public static ClientCompany CreateClientCompany(string name, string description)
        {
            return new ClientCompany()
            {
                Name = name,
                Description = description
            };
        }
    }
}
