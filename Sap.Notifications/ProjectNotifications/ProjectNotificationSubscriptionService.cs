using Abp;
using Abp.Domain.Entities;
using Abp.Notifications;
using Sap.Notifications.NotificationTypes;
using SapModule.Core.Projects.Entities;
using System;
using System.Threading.Tasks;

namespace Sap.Notifications.ProjectNotifications
{
    public class ProjectNotificationSubscriptionService : IProjectNotificationSubscriptionService
    {
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        public ProjectNotificationSubscriptionService(INotificationSubscriptionManager notificationSubscriptionManager)
        {
            _notificationSubscriptionManager = notificationSubscriptionManager;
        }

        public async Task RegisterToGeneralNotifications(Project project, long userId)
        {
            //Register the user
            var userIdentifier = new UserIdentifier(1, userId);
            // To the entity
            // So he doesnt get notifications from another project
            var entityIdentifier = new EntityIdentifier(typeof(Project), project.Id);

            //Register to all todo changes notifications
            //Added to project
            await _notificationSubscriptionManager.SubscribeAsync(userIdentifier,
                  TodoNotificationTypes.AddedToProjectNotification, entityIdentifier);
            //TodoList name changed
            await _notificationSubscriptionManager.SubscribeAsync(userIdentifier,
                  TodoNotificationTypes.TodoListNameChanged, entityIdentifier);
            //Status change
            await _notificationSubscriptionManager.SubscribeAsync(userIdentifier,
                  TodoNotificationTypes.TodoChangeStatusForAllInProject, entityIdentifier);
            //Date set
            await _notificationSubscriptionManager.SubscribeAsync(userIdentifier,
                TodoNotificationTypes.TodoDateEdited, entityIdentifier);

            //Name changed
            await _notificationSubscriptionManager.SubscribeAsync(userIdentifier,
                TodoNotificationTypes.TodoNameEdited, entityIdentifier);

            //Todo deleted
            await
                _notificationSubscriptionManager.SubscribeAsync(userIdentifier,
                TodoNotificationTypes.TodoDeleted,
                    entityIdentifier);
            //Todo discussions
            await
                _notificationSubscriptionManager.SubscribeAsync(userIdentifier,
                TodoNotificationTypes.DiscussionMessage,
                    entityIdentifier);
        }

        public async Task UnregisterMemberFromNotifications(Project project, long userId)
        {
            var userIdentifier = new UserIdentifier(1, userId);
            var entityIdentifier = new EntityIdentifier(typeof(Project), project.Id);
            //Unsubscribe name
            await _notificationSubscriptionManager.UnsubscribeAsync(userIdentifier,
                TodoNotificationTypes.TodoNameEdited, entityIdentifier);
            //Unsubscribe name
            await _notificationSubscriptionManager.UnsubscribeAsync(userIdentifier,
                TodoNotificationTypes.TodoDateEdited, entityIdentifier);
            //Unsubscribe name
            await _notificationSubscriptionManager.UnsubscribeAsync(userIdentifier,
                TodoNotificationTypes.TodoDeleted, entityIdentifier);
            //Unsubscribe name
            await _notificationSubscriptionManager.UnsubscribeAsync(userIdentifier,
                TodoNotificationTypes.TodoChangeStatusForAllInProject, entityIdentifier);
            //Unsubscribe todoListNameChanged
            await
                _notificationSubscriptionManager.UnsubscribeAsync(userIdentifier,
                    TodoNotificationTypes.TodoListNameChanged, entityIdentifier);
        }

        public Task RegisterForBudgetModificationNotifications(Project project, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
