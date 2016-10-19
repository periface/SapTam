using SapModule.Application.Projects.Dto;
using System.Collections.Generic;

namespace SapModule.Application.TodoLists.Dto
{
    public class TodoListOutput
    {
        public IEnumerable<ToDoListDto> ToDoList { get; set; }
    }
}
