using Abp.AutoMapper;
using Abp.Localization;
using Abp.UI;
using Castle.Core.Internal;
using Cinotam.AbpModuleZero.Users;
using Sap.Notifications.ProjectNotifications;
using Sap.Notifications.TodoNotifications;
using Sap.SharedObjects;
using SapModule.Application.GenericInputs;
using SapModule.Application.Todo.Dto;
using SapModule.Core;
using SapModule.Core.Helper;
using SapModule.Core.Helper.Enums;
using SapModule.Core.ProjectMembers;
using SapModule.Core.Projects;
using SapModule.Core.ToDo;
using SapModule.Core.ToDo.Entities;
using SapModule.Core.ToDo.InputHelper;
using SapModule.Core.ToDoLists;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SapModule.Application.Todo
{
    public class TodoService : ITodoService
    {
        //Remember this yo idiot!!!
        //Rules of notifications:
        //All users registered in list must receive notifications about any changes in the todo list
        //Some special notifications must be send to the responsible of the todo 

        //
        private readonly ITodoManager _todoManager;
        private readonly IToDoListManager _toDoListManager;
        private readonly UserManager _userManager;
        private readonly ILocalizationManager _localizationManager;
        private readonly ITodoNotificationsService _todoNotificationsService;
        private readonly IProjectNotificationPublisherService _projectNotificationPublisherService;
        private readonly IProjectManager _projectManager;
        private readonly IStatusResolver _statusResolver;
        private readonly IMembersManager _membersManager;
        public TodoService(ITodoManager todoManager, IToDoListManager toDoListManager, UserManager userManager, IStatusResolver statusResolver, ITodoNotificationsService todoNotificationsService, IProjectNotificationPublisherService projectNotificationPublisherService, IProjectManager projectManager, IMembersManager membersManager, ILocalizationManager localizationManager)
        {
            _todoManager = todoManager;
            _toDoListManager = toDoListManager;
            _userManager = userManager;
            _statusResolver = statusResolver;
            _todoNotificationsService = todoNotificationsService;
            _projectNotificationPublisherService = projectNotificationPublisherService;
            _projectManager = projectManager;
            _membersManager = membersManager;
            _localizationManager = localizationManager;
        }

        public TodoOutput GetTodosFromList(int todoListId)
        {
            var projectId = _toDoListManager.GetProjectIdFromList(todoListId);
            var todos = _todoManager.GetTodos(todoListId).ToList();
            var dtos = todos.Select(a => a.MapTo<TodoDto>());
            var todoDtos = dtos as IList<TodoDto> ?? dtos.ToList();
            todoDtos.ForEach(a => a.Status = _todoManager.GetStatus(a.Id));
            return new TodoOutput()
            {
                ProjectId = projectId,
                TodoDtos = todoDtos
            };
        }

        public TodoOutput GetTodosOfUser(long userId)
        {
            var todos = _todoManager.GetTodos(userId).ToList();
            var projectId = 0;
            if (!todos.Any())
                return new TodoOutput()
                {
                    ProjectId = projectId,
                    TodoDtos = todos.Select(a => a.MapTo<TodoDto>())
                };
            var single = todos.FirstOrDefault();
            if (single == null)
                return new TodoOutput()
                {
                    ProjectId = projectId,
                    TodoDtos = todos.Select(a => a.MapTo<TodoDto>())
                };
            var todoList = _toDoListManager.GetList(single.TodoList.Id);
            projectId = _toDoListManager.GetProjectIdFromList(todoList.Id);
            return new TodoOutput()
            {
                ProjectId = projectId,
                TodoDtos = todos.Select(a => a.MapTo<TodoDto>())
            };
        }

        public async Task<TodoDto> GetTodo(int todoId)
        {
            var todo = _todoManager.GetTodo(todoId);
            var model = todo.MapTo<TodoDto>();
            await ProcessResponsible(model, todo.ResponsibleId);
            ProcessFiles(model);
            ProcessDiscussion(model);
            ProcessStatus(model);
            return model;
        }

        private void ProcessStatus(TodoDto model)
        {
            model.Status = _todoManager.GetStatus(model.Id);
        }


        public int CreateTodo(TodoInput input)
        {
            var todoList = _toDoListManager.GetList(input.TodoListId);
            var todo = Core.ToDo.Entities.Todo.CreateTodo(input.TodoName, todoList);
            var id = _todoManager.CreateEditTodo(todo);
            return id;
        }

        public async Task<MemberDto> SetResponsible(SetResponsibleInput input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var todo = _todoManager.GetTodo(input.TodoId);
            await RegisterAndNotifyAdminAssignation(user, todo);
            var responsible = ResponsibleIdentity.CreateResponsibleIdentity(user, todo);
            _todoManager.SetResponsible(responsible);
            return user.MapTo<MemberDto>();
        }



        public async Task DeleteTodo(int todoId)
        {
            var todo = _todoManager.GetTodo(todoId);
            var todoListId = _todoManager.GetTodoListId(todo);
            var projectId = _toDoListManager.GetProjectIdFromList(todoListId);
            var project = _projectManager.GetProject(projectId);
            _todoManager.DeleteTodo(todoId);

            await _projectNotificationPublisherService.NotifyTodoDeleted(todo, todoListId, project);
        }

        public async Task ChangeByProp(ChangeByPropInput input)
        {
            switch (input.prop)
            {
                case "DueDate":
                    await ProcessDateProp(input);
                    break;
                default:
                    await ProcessStringProp(input);
                    break;
            }

        }


        public long GetPercentageDone(int listId)
        {
            var projectsFinished = _todoManager.GetAllTodos(listId, StatusTypes.Status.Success).Count();
            var allProjects = _todoManager.GetTodos(listId).Count();
            if (allProjects <= 0)
            {
                return 0;
            }
            return (projectsFinished * 100) / allProjects;
        }

        public void ConfigureDiscussionVisibility(bool option, int todoId)
        {
            var todo = _todoManager.GetTodo(todoId);
            todo.ShowDiscussionToClient = option;
            _todoManager.CreateEditTodo(todo);
        }

        public void ConfigureFileVisibility(bool option, int fileId)
        {
            var file = _todoManager.GetFile(fileId);
            file.ShowToClient = true;
            _todoManager.AddTodoFile(file);
        }

        public async Task<DiscussionDto> AddComment(CommentInput input)
        {
            var todo = _todoManager.GetTodo(input.TodoId);
            var discussion = Discussion.CreateDiscussionMessage(input.Comment, todo);
            var id = _todoManager.SendDiscussionMessage(discussion);
            var message = _todoManager.GetMessage(id);
            var model = message.MapTo<DiscussionDto>();
            model.Day = message.CreationTime.Day;
            model.Month = GetNameFromMonth(message.CreationTime.Month);
            if (message.CreatorUserId == null) return model;
            var member = await _userManager.GetUserByIdAsync((long)message.CreatorUserId);
            var todoList = _todoManager.GetTodoListId(todo);
            var projectId = _toDoListManager.GetProjectIdFromList(todoList);
            var project = _projectManager.GetProject(projectId);
            model.Member = member.MapTo<MemberDto>();
            await _todoNotificationsService.SilentChatNotification(model, project);
            return model;
        }

        public async Task<DiscussionOutput> GetDiscussion(int todoId)
        {
            var discussions = _todoManager.GetDiscussion(todoId, 5);
            var discussionsModel = new List<DiscussionDto>();
            foreach (var discussion in discussions)
            {
                var member = new User();
                if (discussion.CreatorUserId != null)
                    member = await _userManager.GetUserByIdAsync((long)discussion.CreatorUserId);
                discussionsModel.Add(new DiscussionDto()
                {
                    Id = discussion.Id,
                    Member = member.MapTo<MemberDto>(),
                    TodoId = todoId,
                    Message = discussion.Message,
                    Day = discussion.CreationTime.Day,
                    Month = GetNameFromMonth(discussion.CreationTime.Month)
                });
            }
            return new DiscussionOutput()
            {
                Discussion = discussionsModel
            };
        }

        public TodoFileOutput AddFiles(int todoId, IEnumerable<FileInput> filesInputs)
        {
            var todo = _todoManager.GetTodo(todoId);
            var todoList = _todoManager.GetTodoListId(todo);
            var projectId = _toDoListManager.GetProjectIdFromList(todoList);
            foreach (var filesInput in filesInputs)
            {
                var file = TodoFile.CreateTodoFile(projectId,
                    filesInput.FileUrl,
                    filesInput.IdServiceFile,
                    filesInput.MimeType,
                    filesInput.MimeType,
                    filesInput.Name,
                    filesInput.SecondaryUrl,
                    filesInput.Icon,
                    filesInput.SourceType,
                    todo);
                _todoManager.AddTodoFile(file);
            }
            var files = _todoManager.GetFiles(todo.Id);

            return new TodoFileOutput()
            {
                TodoFiles = files.Select(a => a.MapTo<TodoFileDto>())
            };
        }

        public TodoFileOutput GetFiles(int todoId)
        {
            var files = _todoManager.GetFiles(todoId);

            return new TodoFileOutput()
            {
                TodoFiles = files.Select(a => a.MapTo<TodoFileDto>())
            };
        }

        public async Task<StatusOutput> GetStatus(int todoId)
        {
            var status = _todoManager.GetStatuses(todoId);
            var message = _localizationManager.GetString(SapConsts.LocalizationSourceName, "StatusChangedByUser");
            var list = new List<StatusDto>();
            foreach (var historyStatus in status)
            {
                var user = new User();
                if (historyStatus.CreatorUserId != null)
                {
                    user = await _userManager.GetUserByIdAsync((long)historyStatus.CreatorUserId);
                }
                var resolvedStatus = _statusResolver.ResolveIntToStatus(historyStatus.StatusType);
                var resolvedName = _statusResolver.GetStatusName(resolvedStatus);
                var resolvedMessage = string.Format(message, user.FullName, resolvedName);

                list.Add(new StatusDto()
                {
                    Member = user,
                    Message = resolvedMessage,
                    StatusType = historyStatus.StatusType
                });

            }
            return new StatusOutput()
            {
                StatusDtos = list
            };
        }

        public async Task SetStatus(int todoId, int status)
        {
            var todo = _todoManager.GetTodo(todoId);
            var statusType = _statusResolver.ResolveIntToStatus(status);
            _todoManager.SetTodoStatus(todoId, statusType);

            var todoListId = _todoManager.GetTodoListId(todo);
            var projectId = _toDoListManager.GetProjectIdFromList(todoListId);
            var project = _projectManager.GetProject(projectId);
            //This goes to notification service
            //Notifies responsible
            //await _todoNotificationsService.NotifyTodoStatusChange(todo, todoListId, statusType);
            //Notifies responsible
            await _projectNotificationPublisherService.NotifyTodoStatusChange(todo, project, statusType);
        }

        /// <summary>
        /// Configures the user subscription to the todo notifications
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task ProcessDateProp(ChangeByPropInput input)
        {
            var stringVal = ((string[])input.value)[0];
            var dateValue = DateTime.Parse(stringVal);
            var todo = _todoManager.GetTodo(input.pk);
            if (todo == null) return;
            var property = todo.GetType().GetProperty(input.prop);
            if (property == null) throw new UserFriendlyException("Property not found");
            property.SetValue(todo, dateValue);
            _todoManager.CreateEditTodo(todo);
            var todoListId = _todoManager.GetTodoListId(todo);
            var projectId = _toDoListManager.GetProjectIdFromList(todoListId);
            var project = _projectManager.GetProject(projectId);
            await _projectNotificationPublisherService.NotifyDateSet(todo, project);
        }
        private async Task ProcessStringProp(ChangeByPropInput input)
        {
            var stringVal = ((string[])input.value)[0];
            var todo = _todoManager.GetTodo(input.pk);
            if (todo == null) return;
            var oldValue = todo.TodoName;
            var property = todo.GetType().GetProperty(input.prop);
            if (property == null) throw new UserFriendlyException("Property not found");
            property.SetValue(todo, stringVal);
            _todoManager.CreateEditTodo(todo);
            var todoListId = _todoManager.GetTodoListId(todo);
            var projectId = _toDoListManager.GetProjectIdFromList(todoListId);
            var project = _projectManager.GetProject(projectId);
            await _projectNotificationPublisherService.NameChanged(todo, oldValue, project);
        }
        private void ProcessDiscussion(TodoDto model)
        {
            return;
        }

        private void ProcessFiles(TodoDto model)
        {
            return;
        }

        private async Task ProcessResponsible(TodoDto model, long? responsibleId)
        {
            if (responsibleId.HasValue)
            {
                var user = await _userManager.GetUserByIdAsync((long)responsibleId);
                model.HasResponsible = true;
                model.Responsible = user.MapTo<MemberDto>();
            }
            else
            {
                model.HasResponsible = false;
            }
        }

        private async Task RegisterAndNotifyAdminAssignation(User user, SapModule.Core.ToDo.Entities.Todo todo)
        {
            await _todoNotificationsService.RegisterToResponsibleNotifications(user, todo);
            await _todoNotificationsService.NotifyTodoResponsibleAssignation(todo);
        }

        private string GetNameFromMonth(int month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }
    }
}
