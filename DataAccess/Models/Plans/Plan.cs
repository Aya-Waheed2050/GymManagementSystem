namespace DataAccess.Models.Plans
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        #region [M - M] Member & Plan
        //Nav Prop
        public ICollection<Membership> Plans { get; set; }

        #endregion

    }
}
