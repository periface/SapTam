using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SapModule.Core.ToDo.Entities;

namespace SapModule.Application.Todo.Dto
{
    public class TodoFileOutput
    {
        public IEnumerable<TodoFileDto> TodoFiles { get; set; }
    }
    [AutoMapFrom(typeof(TodoFile))]
    public class TodoFileDto : EntityDto
    {
        public int ProjectId { get; set; }
        public string FileUrl { get; set; }
        public string IdServiceFile { get; set; }
        public string MimeType { get; set; }
        public string FileType { get; set; }
        public string Name { get; set; }
        public string SecondaryUrl { get; set; }
        public int SourceType { get; set; }
        public bool ShowToClient { get; set; }
        public string Icon { get; set; }
    }
}
