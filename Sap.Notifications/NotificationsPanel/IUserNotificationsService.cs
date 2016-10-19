using Abp.Application.Services;
using Abp.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sap.Notifications.NotificationsPanel
{

    public interface IUserNotificationsService : IApplicationService
    {
        Task<IEnumerable<UserNotification>> GetAllUserNotifications(long userId);
        Task MarkAllAsReaded();
    }
}
