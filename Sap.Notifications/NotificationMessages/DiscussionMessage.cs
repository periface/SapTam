using Abp.Notifications;
using Sap.SharedObjects;

namespace Sap.Notifications.NotificationMessages
{
    public class DiscussionMessage : NotificationData
    {
        public DiscussionMessage(DiscussionDto message)
        {
            Message = message;
        }
        public DiscussionDto Message { get; set; }
    }
}
