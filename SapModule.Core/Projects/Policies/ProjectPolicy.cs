using System;
using Abp.UI;
using SapModule.Core.BudgetInfo.Entities;
using SapModule.Core.Projects.Entities;

namespace SapModule.Core.Projects.Policies
{
    class ProjectPolicy : IProjectPolicy
    {
        public void CheckProjectCreationData(Project project)
        {
            if (project.EndDate < DateTime.Now)
            {
                throw new UserFriendlyException("Project end date error");
            }
        }

        public void CheckBudgetCreationData(Budget budget)
        {
            if (budget.Initial < 0)
            {
                throw new UserFriendlyException("Project budget invalid");
            }
            if (budget.Available < budget.Initial)
            {
                throw new UserFriendlyException("Project budget is over");
            }
        }
    }
}
