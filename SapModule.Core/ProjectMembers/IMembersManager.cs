using Abp.Domain.Services;
using Cinotam.AbpModuleZero.Users;
using SapModule.Core.ProjectMembers.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SapModule.Core.ProjectMembers
{
    public interface IMembersManager : IDomainService
    {
        void AddMember(ProjectMember member);
        Task<IEnumerable<User>> GetAllMembers(int projectId);
        Task<IEnumerable<User>> GetAllCollaborators(int projectId);
        Task<IEnumerable<User>> GetAllMembers();

        Task<IEnumerable<User>> GetLeaders(int projectId);
        User GetLeader(int projectId);
        Task<IEnumerable<User>> GetAllMembers(string searchString);
        User GetMember(int projectId, long userId);
        void RemoveMemberFromProject(long userId, int projectId);
    }
}
