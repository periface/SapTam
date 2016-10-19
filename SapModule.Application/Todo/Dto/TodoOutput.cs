using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sap.SharedObjects;
using System;
using System.Collections.Generic;

namespace SapModule.Application.Todo.Dto
{
    public class TodoOutput
    {
        public int ProjectId { get; set; }
        public IEnumerable<TodoDto> TodoDtos { get; set; }
    }
    [AutoMapFrom(typeof(Core.ToDo.Entities.Todo))]
    public class TodoDto : EntityDto
    {
        public string TodoName { get; set; }
        public long? ResponsibleId { get; set; }
        public DateTime? DueDate { get; set; }
        public int Status { get; set; }
        public string StatusSeverity { get; set; }
        public bool ShowDiscussionToClient { get; set; }
        public bool HasResponsible { get; set; }
        public bool HasFiles { get; set; }
        public bool HasDiscussion { get; set; }
        public MemberDto Responsible { get; set; }
    }
}
