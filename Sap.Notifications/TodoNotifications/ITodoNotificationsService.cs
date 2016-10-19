using Abp.Domain.Services;
using Cinotam.AbpModuleZero.Users;
using Sap.SharedObjects;
using SapModule.Core.Projects.Entities;
using SapModule.Core.ToDo.Entities;
using System.Threading.Tasks;

namespace Sap.Notifications.TodoNotifications
{
    public interface ITodoNotificationsService : IDomainService
    {
        /// <summary>
        /// Register the responsible to its todo notifications
        /// </summary>
        /// <returns></returns>
        Task RegisterToResponsibleNotifications(User user, Todo todo);
        /// <summary>
        /// Notifies the new responsible about his assignation
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        Task NotifyTodoResponsibleAssignation(Todo todo);

        Task SilentChatNotification(DiscussionDto discussionDto, Project project);

    }
}
