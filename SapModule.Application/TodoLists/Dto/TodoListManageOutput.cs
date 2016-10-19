using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SapModule.Core.ToDoLists.Entities;

namespace SapModule.Application.TodoLists.Dto
{
    [AutoMapFrom(typeof(ToDoList))]
    public class TodoListManageOutput : EntityDto
    {
        public int ListId { get; set; }
        public string Name { get; set; }
        public string Observations { get; set; }
        public int ProjectId { get; set; }
    }
}
