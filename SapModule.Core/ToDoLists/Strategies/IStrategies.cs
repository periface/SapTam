using Abp.Domain.Services;
using System;

namespace SapModule.Core.ToDoLists.Strategies
{
    public interface IStrategies : IDomainService
    {
        /// <summary>
        /// Create a strategy based on the status of the project
        /// </summary>
        /// <param name="status"></param>
        /// <param name="todoListId"></param>
        void AssignStrategy(Helper.Enums.StatusTypes.Status status, int todoListId);
        /// <summary>
        /// Adds a background job, when a list is not finished and it exceeds the date, the systems starts to generate loses every x margin days
        /// for a x punish value
        /// </summary>
        /// <param name="todoListsId"></param>
        /// <param name="punishValue"></param>
        /// <param name="margin"></param>
        /// <param name="endDate"></param>
        void InitPunishStrategy(int todoListsId, decimal punishValue, int margin, DateTime endDate);

        void EndPunishStrategy();
    }
}
