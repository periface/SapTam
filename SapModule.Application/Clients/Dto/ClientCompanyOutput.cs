using System.Collections.Generic;
using SapModule.Application.Projects.Dto;

namespace SapModule.Application.Clients.Dto
{
    public class ClientCompanyOutput
    {
        public IEnumerable<ClientCompanyDto> Companies { get; set; }
    }
}
