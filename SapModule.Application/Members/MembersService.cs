using Abp.AutoMapper;
using Sap.Notifications.ProjectNotifications;
using Sap.SharedObjects;
using SapModule.Application.Members.Dto;
using SapModule.Core.ProjectMembers;
using SapModule.Core.ProjectMembers.Entities;
using SapModule.Core.Projects;
using SapModule.Core.Projects.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapModule.Application.Members
{
    public class MembersService : IMembersService
    {
        private readonly IMembersManager _membersManager;
        private readonly IProjectNotificationSubscriptionService _projectNotificationSubscriptionService;
        private readonly IProjectManager _projectManager;
        private readonly IProjectNotificationPublisherService _projectNotificationPublisherService;
        public MembersService(IMembersManager membersManager, IProjectNotificationSubscriptionService projectNotificationSubscriptionService, IProjectManager projectManager, IProjectNotificationPublisherService projectNotificationPublisherService)
        {
            _membersManager = membersManager;
            _projectNotificationSubscriptionService = projectNotificationSubscriptionService;
            _projectManager = projectManager;
            _projectNotificationPublisherService = projectNotificationPublisherService;
        }

        public async Task<MembersOutput> GetMembers(string searchString = "")
        {
            if (string.IsNullOrEmpty(searchString) || searchString == "undefined")
            {
                var members = await _membersManager.GetAllMembers();
                return new MembersOutput()
                {
                    Members = members.Select(a => a.MapTo<MemberDto>())
                };
            }
            else
            {
                var members = await _membersManager.GetAllMembers(searchString);
                return new MembersOutput()
                {
                    Members = members.Select(a => a.MapTo<MemberDto>())
                };
            }
        }
        public async Task<MembersOutput> GetMembersFromProject(int projectId, bool includeLeaders)
        {
            if (includeLeaders)
            {
                var members = await _membersManager.GetAllMembers(projectId);
                return new MembersOutput()
                {
                    Members = members.Select(a => a.MapTo<MemberDto>())
                };
            }
            else
            {
                var members = await _membersManager.GetAllCollaborators(projectId);
                return new MembersOutput()
                {
                    Members = members.Select(a => a.MapTo<MemberDto>())
                };
            }
        }

        public async Task<MemberDto> AddMember(MemberInput input)
        {
            var projectMember = ProjectMember.CreateMember(input.UserId, input.ProjectId, input.Leader);
            _membersManager.AddMember(projectMember);
            var memberAdded = _membersManager.GetMember(input.ProjectId, input.UserId);
            var project = _projectManager.GetProject(input.ProjectId);
            await _projectNotificationSubscriptionService.RegisterToGeneralNotifications(project, input.UserId);

            if (projectMember.Leader)
            {
                await _projectNotificationPublisherService.NotifyAddedAsLeader(project, input.UserId);
            }

            return memberAdded.MapTo<MemberDto>();
        }

        public async Task AddMembers(IEnumerable<MemberInput> input)
        {
            var memberInputs = input as IList<MemberInput> ?? input.ToList();
            Project project = null;
            if (memberInputs.ToList().Any())
            {
                project = _projectManager.GetProject(memberInputs.First().ProjectId);
            }
            foreach (var memberInput in memberInputs)
            {
                var projectMember = ProjectMember.CreateMember(memberInput.UserId, memberInput.ProjectId, memberInput.Leader);
                _membersManager.AddMember(projectMember);

                await _projectNotificationSubscriptionService.RegisterToGeneralNotifications(project, memberInput.UserId);
                if (memberInput.Leader)
                {
                    await _projectNotificationPublisherService.NotifyAddedAsLeader(project, memberInput.UserId);
                }
                else
                {
                    await _projectNotificationPublisherService.NotifyAddedMember(project, memberInput.UserId);
                }

            }
        }

        public MemberDto GetMember(long userId, int projectId)
        {
            var member = _membersManager.GetMember(projectId, userId);
            return member.MapTo<MemberDto>();
        }

        public void RemoveMemberFromProject(int userId, int projectId)
        {
            var project = _projectManager.GetProject(projectId);
            _membersManager.RemoveMemberFromProject(userId, projectId);
            _projectNotificationSubscriptionService.UnregisterMemberFromNotifications(project, userId);
        }
    }
}
