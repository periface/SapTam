using Abp.Domain.Entities.Auditing;

namespace SapModule.Core.BudgetInfo.Entities
{
    public class BudgetInput : FullAuditedEntity
    {
        public decimal Total { get; protected set; }
        public string Concept { get; protected set; }
        public virtual Budget Budget { get; protected set; }
        protected BudgetInput()
        {
            
        }

        public static BudgetInput CreateInput(string concept,decimal total,Budget budget)
        {
            return new BudgetInput()
            {
                Concept = concept,
                Total = total,
                Budget = budget
            };
        }
    }
}
