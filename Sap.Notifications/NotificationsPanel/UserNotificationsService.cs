using Abp;
using Abp.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sap.Notifications.NotificationsPanel
{
    public class UserNotificationsService : IUserNotificationsService
    {
        private readonly IUserNotificationManager _userNotificationManager;

        public UserNotificationsService(IUserNotificationManager userNotificationManager)
        {
            _userNotificationManager = userNotificationManager;

        }

        public async Task<IEnumerable<UserNotification>> GetAllUserNotifications(long userId)
        {
            var userIdentifier = new UserIdentifier(1, userId);
            var notifications = await _userNotificationManager.GetUserNotificationsAsync(userIdentifier);
            return notifications;
        }

        public Task MarkAllAsReaded()
        {
            throw new System.NotImplementedException();
        }

        public async Task MarkAllAsReaded(long userId)
        {
            var userIdentifier = new UserIdentifier(1, userId);
            await _userNotificationManager.UpdateAllUserNotificationStatesAsync(userIdentifier, UserNotificationState.Read);
        }

        public async Task<IEnumerable<UserNotification>> GetLatestUnreadedNotifications(long userId, int take)
        {
            var userIdentifier = new UserIdentifier(1, userId);
            var notifications = await _userNotificationManager.GetUserNotificationsAsync(userIdentifier, UserNotificationState.Unread);
            return notifications;
        }
    }
}
