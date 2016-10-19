using Abp;
using Abp.Domain.Entities;
using Abp.Localization;
using Abp.Notifications;
using Cinotam.AbpModuleZero.Users;
using Sap.Notifications.NotificationMessages;
using Sap.Notifications.NotificationTypes;
using Sap.SharedObjects;
using SapModule.Core;
using SapModule.Core.Helper;
using SapModule.Core.Projects.Entities;
using SapModule.Core.ToDo.Entities;
using System.Threading.Tasks;

namespace Sap.Notifications.TodoNotifications
{
    public class TodoNotificationsService : ITodoNotificationsService
    {
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly INotificationPublisher _notificationPublisher;
        private readonly ILocalizationManager _localizationManager;
        private readonly IStatusResolver _statusResolver;
        public TodoNotificationsService(INotificationSubscriptionManager notificationSubscriptionManager, INotificationPublisher notificationPublisher, ILocalizationManager localizationManager, IStatusResolver statusResolver)
        {
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _notificationPublisher = notificationPublisher;
            _localizationManager = localizationManager;
            _statusResolver = statusResolver;
        }

        public async Task RegisterToResponsibleNotifications(User user, Todo todo)
        {

            var notificationUserIdentity = new UserIdentifier(1, user.Id);
            var entityIdentity = new EntityIdentifier(typeof(Todo), todo.Id);
            if (todo.ResponsibleId.HasValue && user.Id != todo.ResponsibleId)
            {
                //If the todo has already a responsible
                //its diferent we unsubscribe the old responsible from the notifications
                await _notificationSubscriptionManager.UnsubscribeAsync(
                    new UserIdentifier(1, (long)todo.ResponsibleId),
                    TodoNotificationTypes.AddedAsResponsibleOfTask,
                    entityIdentity);
            }
            //As responsible user must be register to his todo notifications
            await _notificationSubscriptionManager.SubscribeAsync(
                notificationUserIdentity,
                TodoNotificationTypes.AddedAsResponsibleOfTask,
                entityIdentity);
        }

        public async Task NotifyTodoResponsibleAssignation(Todo todo)
        {
            if (todo.ResponsibleId.HasValue)
            {
                var message = _localizationManager.GetString(SapConsts.LocalizationSourceName, "AssignedAsLeader");
                var formatedMessage = string.Format(message, todo.TodoName);
                await _notificationPublisher.PublishAsync(TodoNotificationTypes.AddedAsResponsibleOfTask,
                    new AddedAsResponsibleNotificationMessage(todo.Id, todo.TodoList.Id, formatedMessage, todo.TodoName),
                    new EntityIdentifier(typeof(Todo), todo.Id));

            }

        }

        public async Task SilentChatNotification(DiscussionDto discussionDto, Project project)
        {
            //var singleIdentifier = new UserIdentifier(AppConstants.DefaultTenantId, discussionDto.Member.Id);
            //var userIdentifier = new[] { singleIdentifier };
            var entityIdentifier = new EntityIdentifier(typeof(Project), project.Id);
            await
                _notificationPublisher.PublishAsync(TodoNotificationTypes.DiscussionMessage,
                    new DiscussionMessage(discussionDto), entityIdentifier);
        }

        public Task NotifyUserAddedToList(int todoListId)
        {
            return Task.FromResult(0);
        }

        public Task TodoNameChangedNotification(Todo todo, string oldValue)
        {

            var todoIndentityNotification = new EntityIdentifier(typeof(Todo), todo.Id);
            var message = _localizationManager.GetString(SapConsts.LocalizationSourceName, "NameChanged");
            return Task.FromResult(0);
        }

    }
}
