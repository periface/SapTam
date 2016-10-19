using Abp.UI;
using SapModule.Core.Client.Entities;
using System;

namespace SapModule.Core.Client.Policies
{
    public class ClientPolicy : IClientPolicy
    {
        public void CheckCompanyCreation(ClientCompany input)
        {
            if (string.IsNullOrEmpty(input.Name) || string.IsNullOrEmpty(input.Description))
            {
                throw new UserFriendlyException("Info not valid");
            }
        }

        public void CheckClientCreation(ClientInfo input)
        {
            if (input.BirthDate.Year >= DateTime.Now.Year)
            {
                throw new UserFriendlyException("Dude u r a baby!");
            }
        }
    }
}
