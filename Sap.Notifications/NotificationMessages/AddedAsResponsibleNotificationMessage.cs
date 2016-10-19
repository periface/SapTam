using Abp.Notifications;

namespace Sap.Notifications.NotificationMessages
{
    public class AddedAsResponsibleNotificationMessage : NotificationData
    {
        public int TodoId { get; set; }
        public string Message { get; set; }
        public string TodoName { get; set; }
        public int TodoListId { get; set; }

        public AddedAsResponsibleNotificationMessage(int todoId, int todoListId, string message, string todoName)
        {
            TodoId = todoId;
            TodoName = todoName;
            Message = message;
            TodoListId = todoListId;
        }
    }
}
