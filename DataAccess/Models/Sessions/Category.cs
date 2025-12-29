namespace DataAccess.Models.Sessions
{
    public class Category : BaseEntity
    {
        
        public string CategoryName { get; set; } = null!;

        #region [1 - M] Category & Session
        //Nav Prop
        public ICollection<Session> SessionsCate { get; set; }

        #endregion
    }
}
