using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Cinotam.AbpModuleZero.Users;
using SapModule.Core.ProjectMembers.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SapModule.Core.ProjectMembers
{
    public class MembersManager : IMembersManager
    {
        private readonly IRepository<ProjectMember> _projectMemberRepository;
        private readonly IRepository<User, long> _usersRepository;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public MembersManager(IRepository<ProjectMember> projectMemberRepository, UserManager userManager, IRepository<User, long> usersRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _projectMemberRepository = projectMemberRepository;
            _userManager = userManager;
            _usersRepository = usersRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public bool ThereIsAssignation(ProjectMember member)
        {
            var found =
                _projectMemberRepository.GetAllList(a => a.ProjectId == member.ProjectId && a.UserId == member.UserId);
            return found.Count > 0;
        }
        public void AddMember(ProjectMember member)
        {
            if (ThereIsAssignation(member))
            {
                if (member.Leader)
                {
                    var memberAlreadyCreated =
                        _projectMemberRepository.GetAllList(
                            a => a.ProjectId == member.ProjectId && a.UserId == member.UserId);
                    var first = memberAlreadyCreated.FirstOrDefault();
                    first?.MakeLeader();
                    _projectMemberRepository.Update(first);
                    ChangeOtherMemberStatus(member);
                }
            }
            else
            {
                ChangeOtherMemberStatus(member);
                _projectMemberRepository.InsertOrUpdate(member);
            }
            _unitOfWorkManager.Current.SaveChanges();
        }

        private void ChangeOtherMemberStatus(ProjectMember member)
        {
            if (!member.Leader) return;
            var others =
                _projectMemberRepository.GetAllList(a => a.ProjectId == member.ProjectId && a.UserId != member.UserId);
            foreach (var projectMember in others)
            {
                projectMember.UnMakeLeader();
            }
        }


        public async Task<IEnumerable<User>> GetAllMembers(int projectId)
        {
            var members = _projectMemberRepository.GetAllList(a => a.ProjectId == projectId);
            var users = new List<User>();
            foreach (var projectMember in members)
            {
                var user = await _userManager.GetUserByIdAsync(projectMember.UserId);
                users.Add(user);
            }
            return users;
        }

        public async Task<IEnumerable<User>> GetAllCollaborators(int projectId)
        {
            var members = _projectMemberRepository.GetAllList(a => a.ProjectId == projectId && !a.Leader);
            var users = new List<User>();
            foreach (var projectMember in members)
            {
                var user = await _userManager.GetUserByIdAsync(projectMember.UserId);
                users.Add(user);
            }
            return users;
        }

        public async Task<IEnumerable<User>> GetAllMembers()
        {
            var members = await _usersRepository.GetAllListAsync();
            return members;
        }
        public async Task<IEnumerable<User>> GetLeaders(int projectId)
        {
            var members = _projectMemberRepository.GetAllList(a => a.ProjectId == projectId && a.Leader);
            var users = new List<User>();
            foreach (var projectMember in members)
            {
                var user = await _userManager.GetUserByIdAsync(projectMember.UserId);
                users.Add(user);
            }
            return users;
        }

        public User GetLeader(int projectId)
        {
            var userAssignations = _projectMemberRepository.GetAllList(a => a.ProjectId == projectId && a.Leader);
            var singleAssignation = userAssignations.FirstOrDefault();
            if (singleAssignation == null) return null;
            var user = _usersRepository.Get(singleAssignation.UserId);
            return user;
        }

        public async Task<IEnumerable<User>> GetAllMembers(string searchString)
        {
            var members = await _usersRepository.GetAllListAsync(a =>
            a.Name.ToUpper().Contains(searchString.ToUpper())
            ||
            a.Surname.ToUpper().Contains(searchString.ToUpper()));
            return members;
        }

        public User GetMember(int projectId, long userId)
        {
            var userAssignations = _projectMemberRepository.GetAllList(a => a.ProjectId == projectId && a.UserId == userId);
            var singleAssignation = userAssignations.FirstOrDefault();
            if (singleAssignation == null) return null;
            var user = _usersRepository.Get(singleAssignation.UserId);
            return user;
        }

        public void RemoveMemberFromProject(long userId, int projectId)
        {
            var userAssignations =
                _projectMemberRepository.GetAllList(a => a.ProjectId == projectId && a.UserId == userId && !a.Leader);
            foreach (var userAssignation in userAssignations)
            {
                _projectMemberRepository.Delete(userAssignation);
            }
        }
    }
}
