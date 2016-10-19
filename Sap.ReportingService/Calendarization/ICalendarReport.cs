using Abp.Application.Services;
using Sap.ReportingService.Calendarization.Dtos;

namespace Sap.ReportingService.Calendarization
{
    public interface ICalendarReport : IApplicationService
    {
        EventObject GetTodoListCalendarization(int projectId);
        EventObject GetTodosInListCalendarization(int listId);
    }
}
