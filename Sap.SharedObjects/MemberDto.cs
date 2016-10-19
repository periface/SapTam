using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Cinotam.AbpModuleZero.Users;

namespace Sap.SharedObjects
{
    [AutoMap(typeof(User))]
    public class MemberDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName => Name + " " + Surname;
        public string ProfilePicture { get; set; }
    }
}
