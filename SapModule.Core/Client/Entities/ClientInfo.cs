using System;
using Abp.Domain.Entities.Auditing;

namespace SapModule.Core.Client.Entities
{
    public class ClientInfo : FullAuditedEntity
    {
        public string Name { get; protected set; }
        public string LastName { get; protected set; }
        public string Address { get; protected set; }
        public string Gender { get; protected set; }
        public string Email { get; set; }
        public string PhoneNumber { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public string Observations { get; protected set; }
        public string ProfilePicture { get; protected set; }
        public virtual ClientCompany ClientCompany { get; protected set; }

        protected ClientInfo()
        {

        }

        public void AddProfilePicture(string picture)
        {
            ProfilePicture = picture;
        }
        public static ClientInfo CreateClientInfo(string name,
            string lastName,
            string address,
            string gender,
            string phone,
            DateTime birthDate,
            string observations,
            ClientCompany clientCompany)
        {
            return new ClientInfo()
            {
                Address = address,
                Name = name,
                LastName = lastName,
                PhoneNumber = phone,
                Gender = gender,
                BirthDate = birthDate,
                Observations = observations,
                ClientCompany = clientCompany,
                ProfilePicture = "Default"
            };
        }
    }
}
