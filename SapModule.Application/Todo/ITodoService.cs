using Abp.Application.Services;
using Sap.SharedObjects;
using SapModule.Application.GenericInputs;
using SapModule.Application.Todo.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SapModule.Application.Todo
{
    public interface ITodoService : IApplicationService
    {
        /// <summary>
        /// Get todos from the provided list
        /// </summary>
        /// <param name="todoListId"></param>
        /// <returns></returns>
        TodoOutput GetTodosFromList(int todoListId);

        /// <summary>
        /// Get todos assigned to the current user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        TodoOutput GetTodosOfUser(long userId);
        /// <summary>
        /// Gets the todo by id
        /// </summary>
        /// <param name="todoId"></param>
        /// <returns></returns>
        Task<TodoDto> GetTodo(int todoId);
        /// <summary>
        /// Creates a new todo entity
        /// </summary>
        /// <param name="input"></param>
        /// <returns>int</returns>
        int CreateTodo(TodoInput input);

        /// <summary>
        /// Sets the responsible of the task
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<MemberDto> SetResponsible(SetResponsibleInput input);

        Task SetStatus(int todoId, int status);
        Task DeleteTodo(int todoId);
        Task ChangeByProp(ChangeByPropInput input);
        long GetPercentageDone(int listId);
        void ConfigureDiscussionVisibility(bool option, int todoId);
        void ConfigureFileVisibility(bool option, int fileId);
        Task<DiscussionDto> AddComment(CommentInput input);
        Task<DiscussionOutput> GetDiscussion(int todoId);

        TodoFileOutput AddFiles(int todoId, IEnumerable<FileInput> filesInputs);
        TodoFileOutput GetFiles(int todoId);
        Task<StatusOutput> GetStatus(int todoId);
    }
}
