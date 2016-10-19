using System.Collections.Generic;
using Abp.Domain.Services;
using SapModule.Core.BudgetInfo.Entities;
using SapModule.Core.Helper.Enums;
using SapModule.Core.Projects.Entities;

namespace SapModule.Core.Projects
{
    public interface IProjectManager : IDomainService
    {
        int CreateProject(Project project);
        int CreateProject(Project project,decimal? initialBudget);
        void AddClientCompany(int idProject,int idClientCompany);
        void CreateBudget(Budget budget);
        Project GetProject(int idProject);
        IEnumerable<Project> GetProjects(int page, int pageSize, string searchString);
        void ChangeProjectStatus(int projectId,StatusTypes.Status status);
        bool CheckLeaderShip(int id, long? userId);
    }
}
