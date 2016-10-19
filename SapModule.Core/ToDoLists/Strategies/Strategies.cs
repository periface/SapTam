using System;
using SapModule.Core.Helper.Enums;

namespace SapModule.Core.ToDoLists.Strategies
{
    public class Strategies : IStrategies
    {
        public void AssignStrategy(StatusTypes.Status status, int todoListId)
        {
            throw new NotImplementedException();
        }

        public void InitPunishStrategy(int todoListsId, decimal punishValue, int margin, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public void EndPunishStrategy()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send notifications every hour to all workes assigned to this list
        /// </summary>
        private void OnCritical()
        {
            
        }
        /// <summary>
        /// Pause all loses by day in the current list
        /// </summary>
        private void OnPaused()
        {

        }

        private void OnFailed()
        {
            
        }
    }
}
