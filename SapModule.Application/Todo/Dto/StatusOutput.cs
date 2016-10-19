using Cinotam.AbpModuleZero.Users;
using System.Collections.Generic;

namespace SapModule.Application.Todo.Dto
{
    public class StatusOutput
    {
        public IEnumerable<StatusDto> StatusDtos { get; set; }
    }

    public class StatusDto
    {
        public string Message { get; set; }
        public User Member { get; set; }
        public int StatusType { get; set; }
    }
}
