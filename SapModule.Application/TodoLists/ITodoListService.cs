using System.Threading.Tasks;
using Abp.Application.Services;
using SapModule.Application.GenericInputs;
using SapModule.Application.TodoLists.Dto;

namespace SapModule.Application.TodoLists
{
    public interface ITodoListService : IApplicationService
    {
        Task<int> CreateTodoList(TodoListInput input);
        void DeleteList(int id);
        void CreateCopy();
        TodoListOutput GetTodoLists(int idProject);
        TodoListManageOutput GetTodoList(int id);
        void ChangeListStatus(int listId, int status);
        Task ChangeByProp(ChangeByPropInput input);
        bool AllTodosCompleted(int todoListId);
    }
}
