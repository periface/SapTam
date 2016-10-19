using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Cinotam.AbpModuleZero.Users;
using Sap.SharedObjects;
using SapModule.Core.Client.Entities;
using SapModule.Core.Projects.Entities;
using SapModule.Core.ToDoLists.Entities;
using System;
using System.Collections.Generic;

namespace SapModule.Application.Projects.Dto
{
    [AutoMapFrom(typeof(Project))]
    public class ProjectManageDto : EntityDto
    {
        public ProjectManageDto()
        {
            HasTodoTasks = true;
            Collaborators = new List<MemberDto>();
        }
        public string CreatorName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public bool IsBudgetDefined { get; set; }
        public bool HasClientCompany { get; set; }
        public bool HasLeader { get; set; }
        public bool HasCollaborators { get; set; }
        public IEnumerable<MemberDto> Collaborators { get; set; }
        public decimal InitialBudget { get; set; }
        public decimal AvailableBudget { get; set; }
        public IEnumerable<ToDoListDto> ToDoLists { get; set; }
        public ClientCompanyDto ClientCompany { get; set; }
        public LeaderDto Leader { get; set; }
        public bool HasTodoTasks { get; set; }
        public string PublicAccessCode { get; set; }
    }
    [AutoMap(typeof(User))]
    public class LeaderDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName => Name + " " + Surname;
    }
    [AutoMap(typeof(ClientCompany))]
    public class ClientCompanyDto : EntityDto
    {
        public string Name { get; set; }
    }
    [AutoMap(typeof(ToDoList))]
    public class ToDoListDto : EntityDto
    {
        public string Name { get; set; }
        public string Observations { get; set; }
        public int Status { get; set; }
    }
}
