using Abp.AutoMapper;
using Cinotam.AbpModuleZero.Users;
using Sap.SharedObjects;
using SapModule.Application.Projects.Dto;
using SapModule.Core.BudgetInfo;
using SapModule.Core.BudgetInfo.Entities;
using SapModule.Core.Client;
using SapModule.Core.Helper.Enums;
using SapModule.Core.ProjectMembers;
using SapModule.Core.Projects;
using SapModule.Core.Projects.Entities;
using SapModule.Core.ToDoLists;
using SapModule.Core.ToDoLists.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapModule.Application.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectManager _projectManager;
        private readonly IBudgetManager _budgetManager;
        private readonly IClientsManager _clientsManager;
        private readonly UserManager _userManager;
        private readonly IMembersManager _membersManager;
        private readonly IToDoListManager _toDoListManager;
        public ProjectService(IProjectManager projectManager, IBudgetManager budgetManager, UserManager userManager, IClientsManager clientsManager, IMembersManager membersManager, IToDoListManager toDoListManager)
        {
            _projectManager = projectManager;
            _budgetManager = budgetManager;
            _userManager = userManager;
            _clientsManager = clientsManager;
            _membersManager = membersManager;
            _toDoListManager = toDoListManager;
        }

        public ProjectTableOutput GetProjects(int page, int pageSize, string searchString)
        {
            var projects = _projectManager.GetProjects(page, pageSize, searchString);
            return new ProjectTableOutput()
            {
                Projects = projects.Select(a => a.MapTo<ProjectDto>())
            };
        }

        public int CreateProject(ProjectInput input)
        {
            var project = Project.CreateProject(input.Name, input.Description, input.StartDate, input.EndDate, StatusTypes.Status.InProcess);
            var id = _projectManager.CreateProject(project, input.InitialBudget);
            return id;
        }
        public void CreateBudget(BudgetInputDto input)
        {
            var project = _projectManager.GetProject(input.IdProject);
            var budget = Budget.CreateBudget(input.Initial, project);
            _projectManager.CreateBudget(budget);
        }

        public void EditProject(ProjectInput input)
        {
            var project = _projectManager.GetProject(input.Id);
            var editedProject = input.MapTo(project);
            _projectManager.CreateProject(editedProject);
        }

        public ProjectInput GetProjectForEdit(int projectId)
        {
            //Project budget or times can not be edited by any employee
            var project = _projectManager.GetProject(projectId);
            var projectInput = project.MapTo<ProjectInput>();
            return projectInput;
        }

        public async Task<ProjectManageDto> GetProjectDetails(int projectId)
        {
            var project = _projectManager.GetProject(projectId);
            var projectDetails = project.MapTo<ProjectManageDto>();
            await ProcessCreatorName(projectDetails, project.CreatorUserId);
            ProcessBudget(projectDetails, projectId);
            ProcessLeader(projectDetails, projectId);
            await ProcessCollaborators(projectDetails, projectId);
            ProcessClientCompany(projectDetails, projectId);
            ProcessTaskLists(projectDetails, projectId);
            return projectDetails;
        }

        private void ProcessTaskLists(ProjectManageDto projectDetails, int projectId)
        {
            var lists = _toDoListManager.GeToDoLists(projectId);
            var toDoLists = lists as IList<ToDoList> ?? lists.ToList();
            if (toDoLists.Any())
            {
                var todoLists = toDoLists.Select(toDoList => toDoList.MapTo<ToDoListDto>()).ToList();
                projectDetails.ToDoLists = todoLists;
                projectDetails.HasTodoTasks = true;
            }
            else
            {
                projectDetails.HasTodoTasks = false;
            }
        }

        private async Task ProcessCreatorName(ProjectManageDto projectDetails, long? creatorUserId)
        {
            if (creatorUserId != null)
            {
                var user = await _userManager.GetUserByIdAsync(creatorUserId.Value);
                projectDetails.CreatorName = user.FullName;
            }
        }

        private void ProcessClientCompany(ProjectManageDto projectDetails, int idProject)
        {
            var clientCompany = _clientsManager.GetClientCompanyFromProject(idProject);
            if (clientCompany != null)
            {
                projectDetails.ClientCompany = clientCompany.MapTo<ClientCompanyDto>();
                projectDetails.HasClientCompany = true;
            }
            else
            {
                projectDetails.HasClientCompany = false;
            }
        }

        private async Task ProcessCollaborators(ProjectManageDto projectDetails, int idProject)
        {
            var collaborators = await _membersManager.GetAllMembers(idProject);
            var collaboratorsList = collaborators as IList<User> ?? collaborators.ToList();
            if (collaboratorsList.Any())
            {
                projectDetails.Collaborators = collaboratorsList.Select(a => a.MapTo<MemberDto>());
                projectDetails.HasCollaborators = true;
            }
            else
            {

                projectDetails.HasCollaborators = false;
            }
        }

        private void ProcessLeader(ProjectManageDto projectDetails, int idProject)
        {
            var leader = _membersManager.GetLeader(idProject);
            if (leader != null)
            {
                projectDetails.HasLeader = true;
                projectDetails.Leader = leader.MapTo<LeaderDto>();
            }
            else
            {
                projectDetails.HasLeader = false;
            }
        }

        private void ProcessBudget(ProjectManageDto projectDetails, int idProject)
        {
            var budget = _budgetManager.GetBudget(idProject);
            if (budget != null)
            {
                projectDetails.InitialBudget = budget.Initial;
                projectDetails.AvailableBudget = budget.Available;
            }
            else
            {
                projectDetails.IsBudgetDefined = false;
            }
        }
    }
}
