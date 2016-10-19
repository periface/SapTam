using Abp.Domain.Services;
using SapModule.Core.Client.Entities;
using System.Collections.Generic;

namespace SapModule.Core.Client
{
    public interface IClientsManager : IDomainService
    {
        int CreateClientCompany(ClientCompany input);
        void EditClientCompany(ClientCompany input);
        int CreateClient(ClientInfo input);
        void EditClientInfo(ClientInfo input);
        ClientInfo GetClientInfo(int id);
        ClientCompany GetClientCompany(int id);
        IEnumerable<ClientInfo> GetClients();
        IEnumerable<ClientCompany> GetClientCompanies(string searchString = "");
        ClientCompany GetClientCompanyFromProject(int idProject);
    }
}
