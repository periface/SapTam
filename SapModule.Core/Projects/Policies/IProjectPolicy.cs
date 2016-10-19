using Abp.Domain.Services;
using SapModule.Core.BudgetInfo.Entities;
using SapModule.Core.Projects.Entities;

namespace SapModule.Core.Projects.Policies
{
    public interface IProjectPolicy : IDomainService
    {
        void CheckProjectCreationData(Project project);
        void CheckBudgetCreationData(Budget budget);
    }
}
