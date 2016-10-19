using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using SapModule.Core.Client.Entities;
using SapModule.Core.Client.Policies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SapModule.Core.Client
{
    public class ClientsManager : IClientsManager
    {
        private readonly IRepository<ClientCompany> _clientCompanyRepository;
        private readonly IRepository<ClientInfo> _clientInfoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IClientPolicy _clientPolicy;
        public ClientsManager(IRepository<ClientCompany> clientCompanyRepository, IRepository<ClientInfo> clientInfoRepository, IUnitOfWorkManager unitOfWorkManager, IClientPolicy clientPolicy)
        {
            _clientCompanyRepository = clientCompanyRepository;
            _clientInfoRepository = clientInfoRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _clientPolicy = clientPolicy;
        }

        public int CreateClientCompany(ClientCompany input)
        {
            _clientPolicy.CheckCompanyCreation(input);
            var id = _clientCompanyRepository.InsertOrUpdateAndGetId(input);
            _unitOfWorkManager.Current.SaveChanges();
            return id;
        }

        public void EditClientCompany(ClientCompany input)
        {
            throw new NotImplementedException();
        }

        public int CreateClient(ClientInfo input)
        {
            _clientPolicy.CheckClientCreation(input);
            var id = _clientInfoRepository.InsertOrUpdateAndGetId(input);
            _unitOfWorkManager.Current.SaveChanges();
            return id;
        }

        public void EditClientInfo(ClientInfo input)
        {
            throw new NotImplementedException();
        }

        public ClientInfo GetClientInfo(int id)
        {
            return _clientInfoRepository.FirstOrDefault(a => a.Id == id);
        }

        public ClientCompany GetClientCompany(int id)
        {
            return _clientCompanyRepository.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<ClientInfo> GetClients()
        {
            return _clientInfoRepository.GetAllList();
        }

        public IEnumerable<ClientCompany> GetClientCompanies(string searchString = "")
        {
            return string.IsNullOrEmpty(searchString) ? _clientCompanyRepository.GetAllList() : _clientCompanyRepository.GetAllList(a => a.Name.ToUpper().Contains(searchString.ToUpper()));
        }

        public ClientCompany GetClientCompanyFromProject(int idProject)
        {
            var clientCompany = _clientCompanyRepository.GetAll().SingleOrDefault(a => a.Projects.Any(p => p.Id == idProject));
            return clientCompany;
        }
    }
}
