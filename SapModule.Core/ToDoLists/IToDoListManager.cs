using Abp.Domain.Services;
using SapModule.Core.Helper.Enums;
using SapModule.Core.ToDoLists.Entities;
using System.Collections.Generic;

namespace SapModule.Core.ToDoLists
{
    public interface IToDoListManager : IDomainService
    {
        int CreateList(ToDoList input);
        void RemoveDependency(int listId);
        void SetDependency(int listId, int dependsOnListId);
        ToDoList GetList(int listId);
        int GetProjectIdFromList(int listId);
        IEnumerable<ToDoList> GeToDoLists(int projectId);
        void ChangeStatus(int listId, StatusTypes.Status status);
    }
}
