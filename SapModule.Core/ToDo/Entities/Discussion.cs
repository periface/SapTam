using Abp.Domain.Entities.Auditing;
using Abp.UI;

namespace SapModule.Core.ToDo.Entities
{
    public class Discussion : FullAuditedEntity
    {
        public string Message { get; protected set; }
        public Todo Todo { get; protected set; }

        public static Discussion CreateDiscussionMessage(string message, Todo todo)
        {
            if (todo.Id == 0) throw new UserFriendlyException("Todo not found");
            if (string.IsNullOrEmpty(message)) throw new UserFriendlyException("No message found");
            return new Discussion()
            {
                Todo = todo,
                Message = message
            };
        }
    }
}
