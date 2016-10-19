using Abp.Domain.Repositories;
using Abp.UI;
using SapModule.Core.ToDoLists.Entities;
using System;
using System.Linq;

namespace SapModule.Core.ToDoLists.Policies
{
    public class TodoListsPolicy : ITodoListsPolicy
    {
        private readonly IRepository<ToDoList> _todoListRepository;

        public TodoListsPolicy(IRepository<ToDoList> todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        public void ParentListFinished(int? parentId)
        {
            throw new NotImplementedException();
        }

        public void CheckStartDate(ToDoList todoList)
        {
            var project = _todoListRepository.GetAll().Where(a => a.Project.Id == todoList.Project.Id).Select(a => a.Project).ToList();
            var firstOrDefault = project.FirstOrDefault();
            if (firstOrDefault != null)
            {
                if (todoList.EndDate < firstOrDefault.StartDate)
                {
                    throw new UserFriendlyException("La fecha no puede ser menor o igual a la fecha de inicio del proyecto.");
                }
                if (todoList.EndDate > firstOrDefault.EndDate)
                {
                    throw new UserFriendlyException("La fecha no puede ser mayor a la fecha de finalización del proyecto.");
                }
            }
        }

        public void IsLeader(ToDoList list)
        {
            throw new NotImplementedException();
        }
    }
}
