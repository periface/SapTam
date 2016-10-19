using Abp.Domain.Services;
using SapModule.Core.ToDoLists.Entities;

namespace SapModule.Core.ToDoLists.Policies
{
    /// <summary>
    /// Policies:
    /// <para>This functionality is only responsible of show errors if the required data is not correct, if you want to implement strategies check the strategies interface</para>
    /// <para>-A child list cannot start after the parent list has been finished</para>
    /// <para>-A child list date is not before parent list date</para>
    /// <para>-A todolist cannot exceeds the project budget</para>
    /// <para>-If the status of a parent list is changed all child lists must change too</para>
    /// <para>-If a todolist is finished before all its corresponding task these will be finished
    ///  automatically with a special tag [FinishedBefore or something like that] (Maybe this will cause some loses in the budget, maybe not)</para>
    /// <para>-If a todolist is paused users must not be able to interact with it</para>
    /// <para>-If a todolist has been set to critical the system must send notifications to all users about this periodically</para>
    /// </summary>
    public interface ITodoListsPolicy : IDomainService
    {
        void ParentListFinished(int? parentId);
        /// <summary>
        /// Checks if the start date is correct according with the project end and start date, if
        /// the TodoList has a parent list this will also check if the date is correct according with 
        /// the parent
        /// </summary>
        /// <param name="todoList"></param>
        void CheckStartDate(ToDoList todoList);
        void IsLeader(ToDoList list);
    }
}
