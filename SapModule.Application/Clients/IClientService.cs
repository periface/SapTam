using Abp.Application.Services;
using SapModule.Application.Clients.Dto;
using SapModule.Application.Projects.Dto;

namespace SapModule.Application.Clients
{
    public interface IClientService : IApplicationService
    {
        int CreateCompany();
        int CreateClient();
        ClientCompanyDto CreateCompanyAndClient(ClientCompanyInput input);
        ClientCompanyDto AssignClientCompany(AssignationInputDto input);
        ClientCompanyOutput GetClientCompanies(string searchString = "");
    }
}
