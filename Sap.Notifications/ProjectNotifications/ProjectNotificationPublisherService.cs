using Abp;
using Abp.Domain.Entities;
using Abp.Localization;
using Abp.Notifications;
using Sap.Notifications.NotificationMessages;
using Sap.Notifications.NotificationTypes;
using SapModule.Core;
using SapModule.Core.Helper;
using SapModule.Core.Helper.Enums;
using SapModule.Core.Projects.Entities;
using SapModule.Core.ToDo.Entities;
using SapModule.Core.ToDoLists.Entities;
using System.Threading.Tasks;

namespace Sap.Notifications.ProjectNotifications
{
    public class ProjectNotificationPublisherService : IProjectNotificationPublisherService
    {
        private readonly ILocalizationManager _localizationManager;
        private readonly IStatusResolver _statusResolver;
        private readonly INotificationPublisher _notificationPublisher;
        public ProjectNotificationPublisherService(ILocalizationManager localizationManager, IStatusResolver statusResolver, INotificationPublisher notificationPublisher)
        {
            _localizationManager = localizationManager;
            _statusResolver = statusResolver;
            _notificationPublisher = notificationPublisher;
        }

        public async Task NotifyTodoStatusChange(Todo todo, Project project, StatusTypes.Status statusType)
        {
            var message = _localizationManager.GetString(SapConsts.LocalizationSourceName, "StatusChanged");
            var formatedMessage = string.Format(message, todo.TodoName, _statusResolver.GetStatusName(statusType));
            var entityIdentifier = new EntityIdentifier(typeof(Project), project.Id);
            await _notificationPublisher.PublishAsync(
                TodoNotificationTypes.TodoChangeStatusForAllInProject,
                new ChangedStatusNotificationMessage(todo.Id, todo.TodoList.Id, formatedMessage, statusType), entityIdentifier);
        }

        public async Task NotifyTodoDeleted(Todo todo, int todoListId, Project project)
        {
            var message = _localizationManager.GetString(SapConsts.LocalizationSourceName, "TodoDeleted");
            var formatedMessage = string.Format(message, todo.TodoName);
            var entityIdentifier = new EntityIdentifier(typeof(Project), project.Id);
            await _notificationPublisher.PublishAsync(
                TodoNotificationTypes.TodoDeleted,
                new GenericTodoNotification(todo.Id, todoListId, 0, formatedMessage, todo.TodoName), entityIdentifier);
        }

        public async Task NotifyDateSet(Todo todo, Project project)
        {

            var message = _localizationManager.GetString(SapConsts.LocalizationSourceName, "DateChanged");
            var formatedMessage = string.Format(message, todo.TodoName, todo.DueDate?.ToShortDateString());
            var entityIdentifier = new EntityIdentifier(typeof(Project), project.Id);
            await _notificationPublisher.PublishAsync(TodoNotificationTypes.TodoDateEdited, new GenericTodoNotification(todo.Id, todo.TodoList.Id, 0, formatedMessage, todo.TodoName), entityIdentifier);
        }

        public async Task NameChanged(Todo todo, string oldName, Project project)
        {

            var message = _localizationManager.GetString(SapConsts.LocalizationSourceName, "NameChanged");
            var formatedMessage = string.Format(message, oldName, todo.TodoName);
            var entityIdentifier = new EntityIdentifier(typeof(Project), project.Id);
            await _notificationPublisher.PublishAsync(TodoNotificationTypes.TodoNameEdited, new GenericTodoNotification(todo.Id, todo.TodoList.Id, 0, formatedMessage, todo.TodoName), entityIdentifier);
        }

        public async Task NameChangedTodoList(ToDoList toDoList, string oldName, Project project)
        {

            var message = _localizationManager.GetString(SapConsts.LocalizationSourceName, "NameTodoListChanged");
            var formatedMessage = string.Format(message, oldName, toDoList.Name);
            var entityIdentifier = new EntityIdentifier(typeof(Project), project.Id);
            await _notificationPublisher.PublishAsync(TodoNotificationTypes.TodoListNameChanged, new GenericTodoNotification(toDoList.Id, 0, 0, formatedMessage, toDoList.Name), entityIdentifier);
        }

        public async Task NotifyAddedMember(Project project, long userId)
        {
            var message = _localizationManager.GetString(SapConsts.LocalizationSourceName, "AddedToProject");
            var formatedMessage = string.Format(message, project.Name);
            var entityIdentifier = new EntityIdentifier(typeof(Project), project.Id);
            await
                _notificationPublisher.PublishAsync(TodoNotificationTypes.AddedToProjectNotification,
                    new GenericTodoNotification(0, 0, project.Id, formatedMessage, ""), entityIdentifier, userIds: new[] { new UserIdentifier(1, userId), });
        }

        public async Task NotifyAddedAsLeader(Project project, long userId)
        {
            var message = _localizationManager.GetString(SapConsts.LocalizationSourceName, "AddedToProjectAsLeader");
            var formatedMessage = string.Format(message, project.Name);
            var entityIdentifier = new EntityIdentifier(typeof(Project), project.Id);
            await
                _notificationPublisher.PublishAsync(TodoNotificationTypes.AddedAsProjectLeader,
                    new GenericTodoNotification(0, 0, project.Id, formatedMessage, ""), entityIdentifier, userIds: new[] { new UserIdentifier(1, userId), });
        }
    }
}
