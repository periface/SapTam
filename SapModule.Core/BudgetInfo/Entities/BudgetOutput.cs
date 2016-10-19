using System;
using Abp.Domain.Entities.Auditing;
using SapModule.Core.BudgetInfo.Enums;

namespace SapModule.Core.BudgetInfo.Entities
{
    public class BudgetOutput : FullAuditedEntity
    {
        public string Concept { get; protected set; }
        /// <summary>
        /// Types can be: 
        /// <para>Task list</para>
        /// <para>Task</para>
        /// <para>Other</para>
        /// </summary>
        public string Type { get; protected set; }
        public decimal Total { get; protected set; }
        public virtual Budget Budget { get; protected set; }
        protected BudgetOutput()
        {
            
        }

        public static BudgetOutput CreateBudgetOutput(string concept, decimal total, OutputTypes.TaskType type,Budget budget)
        {
            return new BudgetOutput()
            {
                Concept = concept,
                Total = total,
                Type = ResolveType(type),
                Budget = budget
            };
        }

        private static string ResolveType(OutputTypes.TaskType type)
        {
            switch (type)
            {
                case OutputTypes.TaskType.TaskList:
                    return "TaskList";
                case OutputTypes.TaskType.Task:
                    return "Task";
                case OutputTypes.TaskType.Other:
                    return "Other";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
