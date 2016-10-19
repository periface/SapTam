using Abp.Notifications;

namespace Sap.Notifications.NotificationMessages
{
    public class GenericTodoNotification : NotificationData
    {
        public GenericTodoNotification(int todoId, int todoListId, int projectId, string message, string todoName)
        {
            TodoId = todoId;
            TodoName = todoName;
            Message = message;
            TodoListId = todoListId;
            ProjectId = projectId;
        }
        public int TodoId { get; set; }
        public int TodoListId { get; set; }
        public int ProjectId { get; set; }
        public string Message { get; set; }
        public string TodoName { get; set; }
    }
}
