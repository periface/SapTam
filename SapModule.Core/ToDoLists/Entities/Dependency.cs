using Abp.Domain.Entities;

namespace SapModule.Core.ToDoLists.Entities
{
    public class Dependency : Entity
    {
        public int TodoListId { get; set; }
        public int ParentList { get; set; }
    }
}
