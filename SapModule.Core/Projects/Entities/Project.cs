using Abp.Domain.Entities.Auditing;
using SapModule.Core.BudgetInfo.Entities;
using SapModule.Core.Client.Entities;
using SapModule.Core.Helper.Enums;
using System;
using System.Collections.Generic;
using Abp.UI;

namespace SapModule.Core.Projects.Entities
{
    public class Project : FullAuditedEntity
    {
        protected Project()
        {

        }
        public string Name { get; protected set; }
        public int Status { get; protected set; }
        public string Description { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public int TotalTodoList { get; protected set; }
        public int FinishedTodoList { get; protected set; }
        public virtual ICollection<Budget> Budgets { get; protected set; }
        public virtual ClientCompany ClientCompany { get; protected set; }
        public string PublicAccessCode { get; protected set; }

        public void SetStatus(StatusTypes.Status status)
        {
            Status = (int)status;
        }
        public static Project CreateProject(string name, string description, DateTime startDate, DateTime endDate, StatusTypes.Status status, decimal? initialBudget = null)
        {
            return new Project()
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                Status = (int)status,
                TotalTodoList = 0,
                FinishedTodoList = 0,
                PublicAccessCode = CreatePublicAccessCode(startDate, endDate),
            };
        }

        public void SetCompany(ClientCompany company)
        {
            ClientCompany = company;
        }
        public void SetInitialBudget(decimal value)
        {
            if (Id == 0)
            {
                throw new UserFriendlyException("Proyecto no encontrado");
            }
            Budgets = new List<Budget>()
            {
                Budget.CreateBudget(value,this)
            };
        }

        private static string CreatePublicAccessCode(DateTime startDate, DateTime endDate)
        {
            //Five digits guid
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            guid += startDate.DayOfYear + "CODE" + endDate.DayOfYear;

            return guid.ToUpper();
        }

        public string UpdatePublicAccessCode()
        {
            return PublicAccessCode = CreatePublicAccessCode(StartDate, EndDate);
        }
        public decimal GetPercentageDone()
        {
            return 100 * (TotalTodoList / FinishedTodoList);
        }
        public void IncreaseTotalTodoList()
        {
            TotalTodoList++;
        }
        public void DecreaseTotalTodoList()
        {
            TotalTodoList++;
        }
        public void AddFinishedList()
        {
            FinishedTodoList++;
        }
    }
}
