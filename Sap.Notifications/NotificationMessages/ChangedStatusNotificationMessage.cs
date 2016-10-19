using Abp.Notifications;
using SapModule.Core.Helper.Enums;
using System;

namespace Sap.Notifications.NotificationMessages
{
    [Serializable]
    public class ChangedStatusNotificationMessage : NotificationData
    {
        public int TodoId { get; set; }
        public string Message { get; set; }
        public int StatusType { get; set; }
        public int TodoListId { get; set; }
        public ChangedStatusNotificationMessage(int todoId, int todoListId, string message, StatusTypes.Status status)
        {
            TodoId = todoId;
            Message = message;
            StatusType = (int)status;
            TodoListId = todoListId;
        }
    }
}
