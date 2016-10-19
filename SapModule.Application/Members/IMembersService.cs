using Abp.Application.Services;
using Sap.SharedObjects;
using SapModule.Application.Members.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SapModule.Application.Members
{
    public interface IMembersService : IApplicationService
    {
        Task<MembersOutput> GetMembers(string searchString = "");
        Task<MembersOutput> GetMembersFromProject(int projectId, bool includeLeaders);
        Task<MemberDto> AddMember(MemberInput input);
        Task AddMembers(IEnumerable<MemberInput> input);
        MemberDto GetMember(long userId, int projectId);
        void RemoveMemberFromProject(int userId, int projectId);
    }
}
