namespace DataAccess.Models.Health
{
    public class HealthRecord : BaseEntity
    {
       
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }

        #region  [1 - 1] Member & HealtRecord
        //Nav
        public Member MemberHealth { get; set; }
        #endregion

    }
}
