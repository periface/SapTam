namespace SapModule.Application.Members.Dto
{
    public class MemberInput
    {
        public long UserId { get; set; }
        public int ProjectId { get; set; }
        public bool Leader { get; set; }
    }
}
