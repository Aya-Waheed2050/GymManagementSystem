namespace DataAccess.Models.Users
{
    public class Trainer : GymUser
    {
        
        public Specialties Specialties { get; set; }

        #region [1 - M] Trainer & Session
        public ICollection<Session> TrainerSessions { get; set; }
        #endregion
    }
}
