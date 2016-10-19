namespace Sap.Notifications.NotificationTypes
{
    public static class TodoNotificationTypes
    {
        /// <summary>
        /// Notify to all users about any change in any of the todo elements
        /// </summary>
        public static string TodoChangeStatusForAllInProject = "TodoChangeStatusForAllInList";
        /// <summary>
        /// Notifications only for the responsible of the todo element
        /// </summary>
        public static string TodoChangeStatusForResponsibles = "TodoChangeStatus";
        /// <summary>
        /// When a user is added to a project
        /// </summary>
        public static string AddedToProjectNotification = "AddedToProjectNotification";
        /// <summary>
        /// When user is added as responsible of a task
        /// </summary>
        public static string AddedAsResponsibleOfTask = "AddedAsResponsibleOfTask";

        public static string TodoDeleted = "TodoDeleted";
        public static string TodoNameEdited = "TodoNameEdited";
        public static string TodoDateEdited = "TodoDateEdited";
        public static string TodoListNameChanged = "TodoListNameChanged";
        public static string DiscussionMessage = "DiscussionMessage";
        public static string AddedAsProjectLeader = "AddedAsProjectLeaderNotification";
    }
}
