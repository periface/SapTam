using Abp.AutoMapper;
using SapModule.Application.Clients.Dto;
using SapModule.Application.Projects.Dto;
using SapModule.Core.Client;
using SapModule.Core.Client.Entities;
using SapModule.Core.Projects;
using System;
using System.Linq;

namespace SapModule.Application.Clients
{
    public class ClientService : IClientService
    {
        private readonly IClientsManager _clientsManager;
        private readonly IProjectManager _projectManager;
        public ClientService(IClientsManager clientsManager, IProjectManager projectManager)
        {
            _clientsManager = clientsManager;
            _projectManager = projectManager;
        }

        public int CreateCompany()
        {
            throw new NotImplementedException();
        }

        public int CreateClient()
        {
            throw new NotImplementedException();
        }

        public ClientCompanyDto CreateCompanyAndClient(ClientCompanyInput input)
        {
            var company = ClientCompany.CreateClientCompany(input.CompanyName, input.Description);
            _clientsManager.CreateClientCompany(company);
            var client = ClientInfo.CreateClientInfo(
                //Name
                input.Name,
                //Last name
                input.LastName,
                //Address
                "No definido",
                //Gender
                input.Gender,
                //Phone
                "No definido",
                input.BirthDate,
                //Obs
                "No definido",
                company);
            _clientsManager.CreateClient(client);
            _projectManager.AddClientCompany(input.ProjectId, company.Id);
            return company.MapTo<ClientCompanyDto>();
        }

        public ClientCompanyOutput GetClientCompanies(string searchString = "")
        {
            if (searchString == "undefined")
            {
                searchString = String.Empty;
            }
            var companies = _clientsManager.GetClientCompanies(searchString);
            return new ClientCompanyOutput()
            {
                Companies = companies.Select(a => a.MapTo<ClientCompanyDto>())
            };
        }

        public ClientCompanyDto AssignClientCompany(AssignationInputDto input)
        {
            _projectManager.AddClientCompany(input.ProjectId, input.IdClientCompany);
            var company = _clientsManager.GetClientCompany(input.IdClientCompany);
            return company.MapTo<ClientCompanyDto>();
        }
    }
}
