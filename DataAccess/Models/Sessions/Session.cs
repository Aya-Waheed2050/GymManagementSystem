namespace DataAccess.Models.Sessions
{
    public class Session : BaseEntity
    {
       
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #region [1 - M] Category & Session
        //FK
        public int CategoryId { get; set; }

        //Nav Prop
        public Category? Category { get; set; }
        #endregion

        #region [1 - M] Trainer & Session
        //FK
        public int TrainerId { get; set; }

        //Nav prop
        public Trainer? Trainer { get; set; }
        #endregion

        #region [M - M] Member & Session
        public ICollection<MemberSession> MemberSessions { get; set; }
        #endregion
    }
}
