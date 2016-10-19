using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Cinotam.AbpModuleZero.Users;
using SapModule.Core.Helper.Enums;
using SapModule.Core.ToDo.Entities;
using SapModule.Core.ToDoLists.Entities;
using SapModule.Core.ToDoLists.Policies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SapModule.Core.ToDoLists
{
    public class ToDoListManager : IToDoListManager
    {
        private readonly IRepository<ToDoList> _todoListsRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _usersRepository;
        private readonly ITodoListsPolicy _todoListsPolicy;
        private readonly IRepository<Todo> _todoRepository;
        public ToDoListManager(IRepository<ToDoList> todoListsRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<User, long> usersRepository, ITodoListsPolicy todoListsPolicy, IRepository<Todo> todoRepository)
        {
            _todoListsRepository = todoListsRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _usersRepository = usersRepository;
            _todoListsPolicy = todoListsPolicy;
            _todoRepository = todoRepository;
        }

        public int CreateList(ToDoList input)
        {
            if (input.Id == 0)
            {

                _todoListsPolicy.CheckStartDate(input);
            }
            var id = _todoListsRepository.InsertOrUpdateAndGetId(input);
            _unitOfWorkManager.Current.SaveChanges();
            return id;
        }

        public void RemoveDependency(int listId)
        {
            throw new NotImplementedException();
        }

        public void SetDependency(int listId, int dependsOnListId)
        {
            throw new NotImplementedException();
        }


        public ToDoList GetList(int listId)
        {
            var list = _todoListsRepository.Get(listId);
            return list;
        }

        public int GetProjectIdFromList(int listId)
        {
            var project = _todoListsRepository.GetAll().Where(a => a.Id == listId).Select(a => a.Project).ToList();
            var projectFound = project.FirstOrDefault();
            return projectFound?.Id ?? 0;
        }

        public IEnumerable<ToDoList> GeToDoLists(int projectId)
        {
            var lists = _todoListsRepository.GetAll().Where(a => a.Project.Id == projectId);
            return lists.ToList();
        }


        public void ChangeStatus(int listId, StatusTypes.Status status)
        {
            var list = _todoListsRepository.Get(listId);
            var todos = _todoRepository.GetAllList(a => a.TodoList.Id == listId);
            if (status == StatusTypes.Status.Success)
            {
                foreach (var todo in todos)
                {
                    todo.SetStatus(StatusTypes.Status.Success);
                }
            }

            list.SetStatus(status);
        }
    }
}
