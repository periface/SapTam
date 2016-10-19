using Abp.Domain.Services;
using SapModule.Core.Helper.Enums;
using SapModule.Core.ToDo.Entities;
using SapModule.Core.ToDo.InputHelper;
using System;
using System.Collections.Generic;

namespace SapModule.Core.ToDo
{
    public interface ITodoManager : IDomainService
    {
        int CreateEditTodo(Todo input);
        Todo GetTodo(int todoId);
        IEnumerable<Todo> GetTodos(int listId);
        IEnumerable<Todo> GetTodos(long userId);
        void AddTodoFile(TodoFile input);
        int SendDiscussionMessage(Discussion discussionMessage);
        void ChangeDueDate(int todoId, DateTime dueDate);
        void SetResponsible(ResponsibleIdentity input);
        void ChangeTaskName(int todoId, string name);
        void DeleteTodo(int todoId);
        void SetTodoStatus(int todoId, StatusTypes.Status statusType);
        IEnumerable<Todo> GetAllTodos(int todoListId, StatusTypes.Status statusType = StatusTypes.Status.InProcess);
        int GetTodoListId(Todo todo);
        IEnumerable<Discussion> GetDiscussion(int todoId, int? take);

        Discussion GetMessage(int id);
        IEnumerable<TodoFile> GetFiles(int id);
        TodoFile GetFile(int fileId);
        int GetStatus(int todoId);
        IEnumerable<HistoryStatus> GetStatuses(int todoId);
    }
}
