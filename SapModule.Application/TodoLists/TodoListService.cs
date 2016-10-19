using Abp.AutoMapper;
using Sap.Notifications.ProjectNotifications;
using SapModule.Application.GenericInputs;
using SapModule.Application.Projects.Dto;
using SapModule.Application.TodoLists.Dto;
using SapModule.Core.Helper;
using SapModule.Core.ProjectMembers;
using SapModule.Core.Projects;
using SapModule.Core.ToDo;
using SapModule.Core.ToDoLists;
using SapModule.Core.ToDoLists.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SapModule.Application.TodoLists
{
    public class TodoListService : ITodoListService
    {
        private readonly IToDoListManager _toDoListManager;
        private readonly IProjectManager _projectManager;
        private readonly IStatusResolver _statusResolver;
        private readonly IMembersManager _membersManager;
        private readonly IProjectNotificationPublisherService _projectNotificationPublisherService;
        private readonly IProjectNotificationSubscriptionService _projectNotificationSubscriptionService;
        private readonly ITodoManager _todoManager;
        public TodoListService(IToDoListManager toDoListManager, IProjectManager projectManager, IStatusResolver statusResolver, IMembersManager membersManager, IProjectNotificationSubscriptionService projectNotificationSubscriptionService, IProjectNotificationPublisherService projectNotificationPublisherService, ITodoManager todoManager)
        {
            _toDoListManager = toDoListManager;
            _projectManager = projectManager;
            _statusResolver = statusResolver;
            _membersManager = membersManager;
            _projectNotificationSubscriptionService = projectNotificationSubscriptionService;
            _projectNotificationPublisherService = projectNotificationPublisherService;
            _todoManager = todoManager;
        }

        public async Task<int> CreateTodoList(TodoListInput input)
        {
            var finishOnHighPriority = false;
            var finishOnAllPriority = false;
            //Resolve configuration
            switch (input.TaskConfig)
            {
                case "High":
                    finishOnHighPriority = true;
                    break;
                case "All":
                    finishOnAllPriority = true;
                    break;
            }
            var project = _projectManager.GetProject(input.ProjectId);
            var list = ToDoList.CreateList(input.Name, input.Observations, input.StartDate, input.EndDate, 0, finishOnAllPriority, finishOnHighPriority, project, 0, input.AllowTodoSelection, null);
            var id = _toDoListManager.CreateList(list);
            var projectMembers = await _membersManager.GetAllMembers(input.ProjectId);
            foreach (var projectMember in projectMembers)
            {
                await _projectNotificationSubscriptionService.RegisterToGeneralNotifications(project, projectMember.Id);
            }
            //Overkill and unnecessary
            //var listMembers = input.Members.Select(memberInput => new UserTaskListIdentity()
            //{
            //    IsLeader = memberInput.Leader,
            //    TaskListId = id,
            //    UserId = memberInput.UserId
            //}).ToList();
            //_toDoListManager.AddUsersToList(id, listMembers);
            //Register the members to all the notifications of the current list
            //then notifies all
            //await ProcessNotifications(id,listMembers);
            return id;
        }
        public void DeleteList(int id)
        {
            throw new NotImplementedException();
        }
        public void CreateCopy()
        {
            throw new NotImplementedException();
        }
        public TodoListOutput GetTodoLists(int projectId)
        {
            var todoLists = _toDoListManager.GeToDoLists(projectId);
            return new TodoListOutput()
            {
                ToDoList = todoLists.Select(a => a.MapTo<ToDoListDto>())
            };
        }

        public TodoListManageOutput GetTodoList(int id)
        {
            var list = _toDoListManager.GetList(id);
            var model = list.MapTo<TodoListManageOutput>();
            model.ListId = id;
            model.ProjectId = _toDoListManager.GetProjectIdFromList(id);
            return model;
        }

        public void ChangeListStatus(int listId, int statusCode)
        {
            var status = _statusResolver.ResolveIntToStatus(statusCode);
            _toDoListManager.ChangeStatus(listId, status);
        }

        public async Task ChangeByProp(ChangeByPropInput input)
        {
            switch (input.prop)
            {
                case "Name":
                    await ProcessStringProp(input);
                    break;
                default:
                    break;
            }
        }

        public bool AllTodosCompleted(int todoListId)
        {
            return false;
        }

        private async Task ProcessStringProp(ChangeByPropInput input)
        {
            var stringVal = ((string[])input.value)[0];
            var todoList = _toDoListManager.GetList(input.pk);
            var oldValue = todoList.Name;
            if (todoList == null) return;
            var property = todoList.GetType().GetProperty(input.prop);
            if (property == null) return;
            property.SetValue(todoList, stringVal);
            _toDoListManager.CreateList(todoList);
            var projectId = _toDoListManager.GetProjectIdFromList(input.pk);
            var project = _projectManager.GetProject(projectId);
            //Notify about the list change
            await _projectNotificationPublisherService.NameChangedTodoList(todoList, oldValue, project);
        }

        public void FinishTaskList()
        {

        }
    }
}
