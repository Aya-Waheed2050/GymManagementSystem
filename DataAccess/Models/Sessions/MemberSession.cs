namespace DataAccess.Models.Sessions
{
    public class MemberSession : BaseEntity
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }

        public bool IsAttended { get; set; }
    }
}
