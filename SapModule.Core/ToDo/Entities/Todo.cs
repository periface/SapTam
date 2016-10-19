using Abp.Domain.Entities.Auditing;
using Abp.UI;
using SapModule.Core.Helper.Enums;
using SapModule.Core.ToDoLists.Entities;
using System;
using System.Collections.Generic;

namespace SapModule.Core.ToDo.Entities
{
    public class Todo : FullAuditedEntity
    {
        public string TodoName { get; protected set; }
        /// <summary>
        /// Only one user for todo task ???
        /// </summary>
        public long? ResponsibleId { get; protected set; }
        public DateTime? DueDate { get; protected set; }
        public bool ShowDiscussionToClient { get; set; }
        public virtual ToDoList TodoList { get; set; }
        public virtual ICollection<TodoFile> Files { get; protected set; }
        public virtual ICollection<Discussion> Discussions { get; protected set; }
        public virtual ICollection<HistoryStatus> HistoryStatuses { get; protected set; }
        public static Todo CreateTodo(string name, ToDoList list)
        {
            if (list.Id == 0) throw new UserFriendlyException("List not found");
            return new Todo()
            {
                TodoName = name,
                DueDate = null,
                HistoryStatuses = new List<HistoryStatus>()
                {
                    new HistoryStatus()
                    {
                        StatusType = (int)StatusTypes.Status.InProcess
                    }
                },
                ShowDiscussionToClient = false,
                TodoList = list
            };
        }

        public void SetDueDate(DateTime dueDate)
        {
            if (dueDate < DateTime.Now)
            {
                throw new UserFriendlyException("Invalid due date");
            }
            DueDate = dueDate;
        }

        public void SetResponsible(long? responsibleId)
        {
            if (responsibleId == null) throw new ArgumentNullException(nameof(responsibleId));
            ResponsibleId = responsibleId;
        }

        public void SetName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                TodoName = name;
            }
        }

        public void SetStatus(StatusTypes.Status statusType)
        {
            HistoryStatuses.Add(new HistoryStatus()
            {
                StatusType = (int)statusType,
            });
        }
    }
}
