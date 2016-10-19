using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Logging;
using Abp.UI;
using SapModule.Core.BudgetInfo.Entities;
using SapModule.Core.Client.Entities;
using SapModule.Core.Helper.Enums;
using SapModule.Core.ProjectMembers.Entities;
using SapModule.Core.Projects.Entities;
using SapModule.Core.Projects.Policies;
using System.Collections.Generic;
using System.Linq;

namespace SapModule.Core.Projects
{
    public class ProjectManager : IProjectManager
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Budget> _budgetRepository;
        private readonly IRepository<ClientCompany> _clientCompanyRepository;
        private readonly IProjectPolicy _projectPolicy;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<ProjectMember> _projectMemberRepository;
        public ProjectManager(IRepository<Project> projectRepository, IUnitOfWorkManager unitOfWorkManager, IProjectPolicy projectPolicy, IRepository<Budget> budgetRepository, IRepository<ClientCompany> clientCompanyRepository, IRepository<ProjectMember> projectMemberRepository)
        {
            _projectRepository = projectRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _projectPolicy = projectPolicy;
            _budgetRepository = budgetRepository;
            _clientCompanyRepository = clientCompanyRepository;
            _projectMemberRepository = projectMemberRepository;
        }

        public int CreateProject(Project project)
        {
            _projectPolicy.CheckProjectCreationData(project);
            var id = _projectRepository.InsertOrUpdateAndGetId(project);
            _unitOfWorkManager.Current.SaveChanges();
            return id;
        }
        /// <summary>
        /// Also works for edit
        /// </summary>
        /// <param name="project"></param>
        /// <param name="initialBudget"></param>
        /// <returns></returns>
        public int CreateProject(Project project, decimal? initialBudget)
        {
            _projectPolicy.CheckProjectCreationData(project);
            var id = _projectRepository.InsertOrUpdateAndGetId(project);
            _unitOfWorkManager.Current.SaveChanges();
            if (initialBudget.HasValue)
            {
                project.SetInitialBudget(initialBudget.Value);
            }
            return id;
        }

        public void AddClientCompany(int idProject, int idClientCompany)
        {
            var project = _projectRepository.Get(idProject);
            var company = _clientCompanyRepository.Get(idClientCompany);
            project.SetCompany(company);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public void CreateBudget(Budget budget)
        {
            _projectPolicy.CheckBudgetCreationData(budget);
            _budgetRepository.InsertOrUpdateAndGetId(budget);
        }
        public Project GetProject(int idProject)
        {
            var project = _projectRepository.FirstOrDefault(a => a.Id == idProject);
            if (project == null) throw new UserFriendlyException("Project not found!", LogSeverity.Warn);
            return project;
        }

        public IEnumerable<Project> GetProjects(int page, int pageSize, string searchString)
        {
            return _projectRepository.GetAllList();
        }

        public void ChangeProjectStatus(int projectId, StatusTypes.Status status)
        {
            var project = _projectRepository.Get(projectId);
            project.SetStatus(status);
        }

        public bool CheckLeaderShip(int id, long? userId)
        {
            var member = _projectMemberRepository.GetAllList(a => a.UserId == userId && a.ProjectId == id);
            if (!member.Any()) return false;
            var first = member.FirstOrDefault(a => a.Leader);
            return first != null;
        }
    }
}
