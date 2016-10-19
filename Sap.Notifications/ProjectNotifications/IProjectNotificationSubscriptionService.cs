using Abp.Domain.Services;
using SapModule.Core.Projects.Entities;
using System.Threading.Tasks;

namespace Sap.Notifications.ProjectNotifications
{
    public interface IProjectNotificationSubscriptionService : IDomainService
    {
        Task RegisterToGeneralNotifications(Project project, long userId);
        Task UnregisterMemberFromNotifications(Project project, long userId);
        Task RegisterForBudgetModificationNotifications(Project project, long userId);
    }
}
