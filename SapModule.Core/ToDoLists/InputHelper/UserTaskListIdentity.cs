namespace SapModule.Core.ToDoLists.InputHelper
{
    public class UserTaskListIdentity
    {
        public long UserId { get; set; }
        public int TaskListId { get; set; }
        public bool IsLeader { get; set; }
    }
}
