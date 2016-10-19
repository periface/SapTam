using Abp.Domain.Services;
using SapModule.Core.Client.Entities;

namespace SapModule.Core.Client.Policies
{
    public interface IClientPolicy : IDomainService
    {
        void CheckCompanyCreation(ClientCompany input);
        void CheckClientCreation(ClientInfo input);
    }
}
