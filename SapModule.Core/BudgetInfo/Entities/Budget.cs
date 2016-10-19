using Abp.Domain.Entities.Auditing;
using Abp.UI;
using SapModule.Core.Projects.Entities;
using System.Collections.Generic;

namespace SapModule.Core.BudgetInfo.Entities
{
    public class Budget : FullAuditedEntity
    {
        protected Budget()
        {
            Default = true;
        }
        public decimal Initial { get; protected set; }
        public decimal Available { get; protected set; }
        public decimal Used { get; protected set; }
        public Project Project { get; protected set; }
        public bool Default { get; protected set; }
        public virtual ICollection<BudgetOutput> BudgetOutputs { get; protected set; }
        public virtual ICollection<BudgetInput> BudgetInputs { get; protected set; }
        public static Budget CreateBudget(decimal initial, Project project)
        {
            if (project.Id == 0)
            {
                throw new UserFriendlyException("Project not found");
            }
            return new Budget()
            {
                Project = project,
                Available = initial,
                Initial = initial,
                Used = 0
            };
        }
    }
}
