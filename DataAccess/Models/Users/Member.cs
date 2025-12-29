namespace DataAccess.Models.Users
{
    public class Member : GymUser
    {
        public string Photo { get; set; } = null!;

        #region  [1 - 1] Member & HealtRecord
        //Nav
        public HealthRecord HealthRecord { get; set; }
        #endregion

        #region [M - M] Member & Plan
        //Nav Prop
        public ICollection<Membership> Members { get; set; }

        #endregion

        #region [M - M] Member & Session
        public ICollection<MemberSession> MemberSessions { get; set; }
        #endregion

    }
}
