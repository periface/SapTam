using Sap.SharedObjects;
using System.Collections.Generic;

namespace SapModule.Application.Members.Dto
{

    public class MembersOutput
    {
        public IEnumerable<MemberDto> Members { get; set; }
    }
}
