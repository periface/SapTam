using Abp.Domain.Repositories;
using SapModule.Core.ToDo.Entities;

namespace SapModule.Core.ToDo.Policies
{
    public class TodoPolicy : ITodoPolicy
    {
        private IRepository<Todo> _todoRepository;

        public TodoPolicy(IRepository<Todo> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public void CheckTodoCreation(Todo input)
        {
            return;
        }
    }
}
