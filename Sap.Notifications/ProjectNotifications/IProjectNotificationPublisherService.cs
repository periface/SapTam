using Abp.Domain.Services;
using SapModule.Core.Helper.Enums;
using SapModule.Core.Projects.Entities;
using SapModule.Core.ToDo.Entities;
using SapModule.Core.ToDoLists.Entities;
using System.Threading.Tasks;

namespace Sap.Notifications.ProjectNotifications
{
    public interface IProjectNotificationPublisherService : IDomainService
    {
        Task NotifyTodoStatusChange(Todo todo, Project project, StatusTypes.Status statusType);


        Task NotifyTodoDeleted(Todo todo, int todoListId, Project project);
        Task NotifyDateSet(Todo todo, Project project);

        Task NameChanged(Todo todo, string oldName, Project project);
        Task NameChangedTodoList(ToDoList toDoList, string oldName, Project project);
        Task NotifyAddedMember(Project project, long userId);
        Task NotifyAddedAsLeader(Project project, long userId);
    }
}
