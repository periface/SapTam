using Abp.Domain.Entities.Auditing;
using Abp.UI;
using SapModule.Core.Helper.Enums;
using SapModule.Core.Projects.Entities;
using SapModule.Core.ToDo.Entities;
using System;
using System.Collections.Generic;

namespace SapModule.Core.ToDoLists.Entities
{
    /// <summary>
    /// Dev: Alan Torres
    /// Entity: TodoList 
    /// DependsOn: Project
    /// FunctionalityCreatedForThis.- LaboralDaysManager : Check LaboralDaysManager Folder
    /// Todo: We need to move the budget properties to another entity, 
    /// Todo: the background worker will fill all the budget data every day, so it needs to have a history
    /// Todo: Task list leader can add a budget output of any value inside the list available budget
    /// Note: The functionality for this class must not be implemented until its finished
    /// </summary>
    public class ToDoList : FullAuditedEntity
    {
        protected ToDoList()
        {

        }
        public Project Project { get; protected set; }
        /// <summary>
        /// Name of the task list
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Observations of the task lis
        /// </summary>
        public string Observations { get; protected set; }
        /// <summary>
        /// Start date of the list
        /// </summary>
        public DateTime StartAt { get; protected set; }
        /// <summary>
        /// Estimated end date of the list
        /// </summary>
        public DateTime EndDate { get; protected set; }
        /// <summary>
        /// The date when the list was set to finished
        /// </summary>
        public DateTime? FinishedOn { get; protected set; }
        /// <summary>
        /// Enable or disable punish configuration
        /// </summary>
        public bool PunishUnfinishedOnDate { get; protected set; }
        /// <summary>
        /// Punish task list when the time is over by day
        /// </summary>
        public decimal? PunishConfig { get; protected set; }
        /// <summary>
        /// Punish will be set every {0} days
        /// </summary>
        public int MarginDays { get; protected set; }
        /// <summary>
        /// Todolist will be set to finished when all high priority task are done
        /// </summary>
        public bool FinishOnHighPriorityTaskDone { get; protected set; }
        /// <summary>
        /// Todolist will be set to finished when all tasks are done, this overrides highpriority requerimients
        /// </summary>
        public bool FinishOnAllTasksDone { get; protected set; }
        /// <summary>
        /// Estimated cost by day used for the task (also depends on ignored weekends config)
        /// </summary>
        public decimal CostByDay { get; protected set; }
        public void FinishTodoList()
        {
            FinishedOn = DateTime.Now;
            Status = (int)StatusTypes.Status.Success;
            UnPunish();
        }
        /// <summary>
        /// Budget depends on the available budget of the project
        /// </summary>
        public decimal? Budget { get; protected set; }
        /// <summary>
        /// Budget already used by the task list calculated every day
        /// </summary>
        public decimal UsedBudget { get; protected set; }
        /// <summary>
        /// Available budget (depends on used budget, calculated every day)
        /// </summary>
        public decimal AvailableBudget { get; protected set; }
        /// <summary>
        /// Enable or disable working weekends set to false by default
        /// </summary>
        public bool EnableWeekends { get; protected set; }
        /// <summary>
        /// Status of the project
        /// </summary>
        public int Status { get; protected set; }
        public bool AllowTodoSelection { get; protected set; }
        public virtual ICollection<Todo> Todos { get; set; }
        public static ToDoList CreateList(string name, string observations, DateTime startsAt, DateTime endsAt, int punishMargin, bool finishOnAllTaskDone, bool finishOnHighPriority, Project project, int laboralDays, bool allowTodoSelection, decimal? budget = null, decimal? punishValue = null, int? dependsOn = null)
        {
            if (project.Id == 0) throw new UserFriendlyException("Project not found");
            return new ToDoList()
            {
                Budget = budget,
                StartAt = startsAt,
                EndDate = endsAt,
                Observations = observations,
                Name = name,
                Project = project,
                Status = (int)StatusTypes.Status.InProcess,
                PunishConfig = punishValue,
                PunishUnfinishedOnDate = punishValue.HasValue,
                MarginDays = punishMargin,
                CostByDay = SetCostByDay(budget, laboralDays),
                AvailableBudget = budget ?? 0,
                UsedBudget = 0,
                EnableWeekends = false,
                FinishOnAllTasksDone = true,
                FinishOnHighPriorityTaskDone = false,
                AllowTodoSelection = allowTodoSelection
            };
        }
        private static decimal SetCostByDay(decimal? budget, int laboralDays)
        {
            if (!budget.HasValue) return 0;
            var total = budget.Value / laboralDays;
            return total;
        }
        public void IncreaseBudget(decimal budget, int laboralDays)
        {
            Budget = budget - UsedBudget;
            CostByDay = SetCostByDay(Budget, laboralDays);
        }
        public void SetStatus(StatusTypes.Status status)
        {
            Status = (int)status;
        }
        public void SetFinishOnAllTaskDone()
        {
            FinishOnAllTasksDone = true;
            FinishOnHighPriorityTaskDone = false;
        }
        public void UnPunish()
        {
            PunishUnfinishedOnDate = false;
        }
        public void SetPunish(decimal? punishValue, int punishMargin)
        {
            PunishUnfinishedOnDate = true;
            PunishConfig = punishValue;
            MarginDays = punishMargin;
        }
        public void SetFinishOnHighPriorityTaskDone()
        {
            FinishOnHighPriorityTaskDone = true;
            FinishOnAllTasksDone = false;
        }

    }
}
