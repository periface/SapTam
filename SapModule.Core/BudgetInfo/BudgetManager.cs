using System;
using System.Linq;
using Abp.Domain.Repositories;
using SapModule.Core.BudgetInfo.Entities;

namespace SapModule.Core.BudgetInfo
{
    public class BudgetManager : IBudgetManager
    {
        private readonly IRepository<Budget> _budgetRepository;

        public BudgetManager(IRepository<Budget> budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public void AddInput(BudgetInput budgetInput)
        {
            throw new NotImplementedException();
        }

        public void AddOutput()
        {
            throw new NotImplementedException();
        }

        public Budget GetBudget(int idProject)
        {
            var budget = _budgetRepository.GetAll().Where(a => 
            a.Project.Id == idProject && a.Default)
            .ToList()
            .FirstOrDefault();
            return budget;
        }
    }
}
