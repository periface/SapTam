using System;

namespace SapModule.Application.Clients.Dto
{
    public class ClientCompanyInput
    {
        public int ProjectId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
