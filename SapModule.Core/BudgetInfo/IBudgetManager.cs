using Abp.Domain.Services;
using SapModule.Core.BudgetInfo.Entities;

namespace SapModule.Core.BudgetInfo
{
    public interface IBudgetManager : IDomainService
    {
        void AddInput(BudgetInput budgetInput);
        void AddOutput();
        Budget GetBudget(int idProject);
    }
}
