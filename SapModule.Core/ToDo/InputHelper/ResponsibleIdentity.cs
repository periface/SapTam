using Abp.UI;
using Cinotam.AbpModuleZero.Users;
using SapModule.Core.ToDo.Entities;

namespace SapModule.Core.ToDo.InputHelper
{
    public class ResponsibleIdentity
    {
        public long UserId { get; protected set; }
        public int TodoId { get; protected set; }

        public static ResponsibleIdentity CreateResponsibleIdentity(User user, Todo todo)
        {
            if (user.Id == 0 || todo.Id == 0)
            {
                throw new UserFriendlyException("Invalid user or todo");
            }
            return new ResponsibleIdentity()
            {
                UserId = user.Id,
                TodoId = todo.Id
            };
        }
    }
}
