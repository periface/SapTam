using Abp.Domain.Entities.Auditing;
using Abp.UI;

namespace SapModule.Core.ToDo.Entities
{
    public class TodoFile : FullAuditedEntity
    {
        protected TodoFile()
        {

        }
        /// <summary>
        /// Can be usefull for linking
        /// </summary>
        public int ProjectId { get; protected set; }
        public string FileUrl { get; protected set; }
        public string IdServiceFile { get; protected set; }
        public string MimeType { get; protected set; }
        public string FileType { get; protected set; }
        public string Name { get; protected set; }
        public string SecondaryUrl { get; protected set; }
        public int SourceType { get; protected set; }
        public bool ShowToClient { get; set; }
        public virtual Todo Todo { get; protected set; }
        public string Icon { get; protected set; }
        public static TodoFile CreateTodoFile(int projectId, string fileUrl,
            string idServiceFile, string mimeType, string fileType, string name, string secondaryUrl, string icon, int fileSource, Todo todo)
        {
            if (todo.Id == 0)
            {
                throw new UserFriendlyException("Todo not found!");
            }
            return new TodoFile()
            {
                ProjectId = projectId,
                FileUrl = fileUrl,
                IdServiceFile = idServiceFile,
                MimeType = mimeType,
                FileType = fileType,
                Name = name,
                SecondaryUrl = secondaryUrl,
                SourceType = fileSource,
                ShowToClient = false,
                Icon = icon,
                Todo = todo
            };
        }
    }
}
