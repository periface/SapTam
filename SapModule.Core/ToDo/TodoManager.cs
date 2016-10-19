using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using SapModule.Core.Helper.Enums;
using SapModule.Core.ToDo.Entities;
using SapModule.Core.ToDo.InputHelper;
using SapModule.Core.ToDo.Policies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SapModule.Core.ToDo
{
    public class TodoManager : ITodoManager
    {
        private readonly IRepository<Todo> _todoRepository;
        private readonly IRepository<TodoFile> _todoFileRepository;
        private readonly IRepository<Discussion> _discussionRepository;
        private readonly ITodoPolicy _todoPolicy;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<HistoryStatus> _statusRepository;
        public TodoManager(IRepository<Todo> todoRepository, IRepository<TodoFile> todoFileRepository, IRepository<Discussion> discussionRepository, IUnitOfWorkManager unitOfWorkManager, ITodoPolicy todoPolicy, IRepository<HistoryStatus> statusRepository)
        {
            _todoRepository = todoRepository;
            _todoFileRepository = todoFileRepository;
            _discussionRepository = discussionRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _todoPolicy = todoPolicy;
            _statusRepository = statusRepository;
        }

        public int CreateEditTodo(Todo input)
        {
            _todoPolicy.CheckTodoCreation(input);
            var id = _todoRepository.InsertOrUpdateAndGetId(input);
            _unitOfWorkManager.Current.SaveChanges();
            return id;
        }

        public Todo GetTodo(int todoId)
        {
            var todo = _todoRepository.FirstOrDefault(a => a.Id == todoId);
            return todo;
        }

        public IEnumerable<Todo> GetTodos(int listId)
        {
            var todos = _todoRepository.GetAll().Where(a => a.TodoList.Id == listId);
            return todos;
        }
        public IEnumerable<Todo> GetTodos(long userId)
        {
            var todos = _todoRepository.GetAll().Where(a => a.ResponsibleId == userId);
            return todos;
        }
        public void AddTodoFile(TodoFile input)
        {
            _todoFileRepository.InsertOrUpdateAndGetId(input);
            _unitOfWorkManager.Current.SaveChanges();
        }
        public int SendDiscussionMessage(Discussion discussionMessage)
        {
            var message = Discussion.CreateDiscussionMessage(discussionMessage.Message, discussionMessage.Todo);
            var id = _discussionRepository.InsertOrUpdateAndGetId(message);
            _unitOfWorkManager.Current.SaveChanges();
            return id;
        }

        public void ChangeDueDate(int todoId, DateTime dueDate)
        {
            var todo = _todoRepository.Get(todoId);
            todo.SetDueDate(dueDate);
        }

        public void SetResponsible(ResponsibleIdentity input)
        {
            var todo = _todoRepository.Get(input.TodoId);
            todo.SetResponsible(input.UserId);
        }

        public void ChangeTaskName(int todoId, string name)
        {
            var todo = _todoRepository.Get(todoId);
            todo.SetName(name);
        }

        public void DeleteTodo(int todoId)
        {
            _todoRepository.Delete(todoId);
        }

        public void SetTodoStatus(int todoId, StatusTypes.Status statusType)
        {
            var todo = _todoRepository.Get(todoId);
            todo.SetStatus(statusType);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public IEnumerable<Todo> GetAllTodos(int todoListId, StatusTypes.Status statusType = StatusTypes.Status.InProcess)
        {
            var todos = _todoRepository.GetAll();
            var results = from todo in todos
                          where todo.HistoryStatuses.OrderByDescending(a => a.CreationTime).FirstOrDefault()
                                != null &&
                                todo.HistoryStatuses.OrderByDescending(a => a.CreationTime).FirstOrDefault().StatusType ==
                                (int)statusType
                          select todo;
            return results.ToList();
        }

        public int GetTodoListId(Todo todo)
        {
            var todoLists = _todoRepository.GetAll().Where(a => a.Id == todo.Id).Select(a => a.TodoList).ToList();
            if (todoLists.Any())
            {
                var todoList = todoLists.FirstOrDefault();
                if (todoList != null) return todoList.Id;
            }
            return 0;
        }

        public IEnumerable<Discussion> GetDiscussion(int todoId, int? take)
        {
            var discussions = _discussionRepository.GetAllList(a => a.Todo.Id == todoId)
                .OrderByDescending(a => a.CreationTime);
            if (take.HasValue)
            {
                return discussions.Take(take.Value).ToList();
            }
            return discussions;
        }

        public Discussion GetMessage(int id)
        {
            return _discussionRepository.Get(id);
        }

        public IEnumerable<TodoFile> GetFiles(int id)
        {
            var files = _todoFileRepository.GetAllList(a => a.Todo.Id == id).OrderByDescending(a => a.CreationTime);
            return files;
        }

        public TodoFile GetFile(int fileId)
        {
            return _todoFileRepository.Get(fileId);
        }

        public int GetStatus(int todoId)
        {
            var status = _statusRepository.GetAll()
                .Where(a => a.Todo.Id == todoId)
                .OrderByDescending(a => a.CreationTime)
                .FirstOrDefault();
            return status?.StatusType ?? 0;
        }

        public IEnumerable<HistoryStatus> GetStatuses(int todoId)
        {
            return _statusRepository.GetAllList(a => a.Todo.Id == todoId);
        }
    }
}
