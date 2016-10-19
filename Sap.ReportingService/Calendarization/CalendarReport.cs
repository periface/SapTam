using Abp.Domain.Repositories;
using Sap.ReportingService.Calendarization.Dtos;
using SapModule.Core.ToDo.Entities;
using SapModule.Core.ToDoLists.Entities;
using System.Linq;

namespace Sap.ReportingService.Calendarization
{
    public class CalendarReport : ICalendarReport
    {
        private readonly IRepository<ToDoList> _todoListRepository;
        private readonly IRepository<Todo> _todoRepository;
        public CalendarReport(IRepository<ToDoList> todoListRepository, IRepository<Todo> todoRepository)
        {
            _todoListRepository = todoListRepository;
            _todoRepository = todoRepository;
        }

        public EventObject GetTodoListCalendarization(int projectId)
        {
            var todoLists = _todoListRepository.GetAllList(a => a.Project.Id == projectId);
            return new EventObject()
            {
                EventsDto = todoLists.Select(a => new EventDto()
                {
                    EndDate = a.EndDate,
                    Id = a.Id,
                    StartDate = a.StartAt,
                    Title = a.Name,
                    Url = ""
                })
            };
        }

        public EventObject GetTodosInListCalendarization(int listId)
        {
            var todos = _todoRepository.GetAllList(a => a.TodoList.Id == listId);
            return new EventObject()
            {
                EventsDto = todos.Select(a => new EventDto()
                {
                    EndDate = a.CreationTime,
                    Id = a.Id,
                    StartDate = a.DueDate ?? a.CreationTime,
                    Title = a.TodoName,
                    Url = ""
                })
            };
        }
    }
}
