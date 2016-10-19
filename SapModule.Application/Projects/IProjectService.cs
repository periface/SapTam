using System.Threading.Tasks;
using Abp.Application.Services;
using SapModule.Application.Projects.Dto;

namespace SapModule.Application.Projects
{
    public interface IProjectService : IApplicationService
    {
        ProjectTableOutput GetProjects(int page,int pageSize,string searchString);
        int CreateProject(ProjectInput input);
        void CreateBudget(BudgetInputDto input);
        void EditProject(ProjectInput input);
        ProjectInput GetProjectForEdit(int projectId);
        Task<ProjectManageDto> GetProjectDetails(int projectId);
    }
}
