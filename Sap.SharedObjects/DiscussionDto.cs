using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SapModule.Core.ToDo.Entities;

namespace Sap.SharedObjects
{
    [AutoMap(typeof(Discussion))]
    public class DiscussionDto : EntityDto
    {
        public string Message { get; set; }
        public int TodoId { get; set; }
        public MemberDto Member { get; set; }
        public string Month { get; set; }
        public int Day { get; set; }
    }
}
