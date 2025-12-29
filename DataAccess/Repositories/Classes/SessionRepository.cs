namespace DataAccess.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymSystemDbContext _dbContext;
        public SessionRepository(GymSystemDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
            return _dbContext.Sessions
                .Include(S => S.Trainer)
                .Include(S => S.Category)
                .ToList();
        }

        public int GetCountOfBookSlots(int SessionId)
        {
            return _dbContext.MemberSessions.Count(X => X.SessionId == SessionId);
        }

        public Session? GetSessionWithTrainerAndCategoryById(int id)
        {
            return _dbContext.Sessions
                .Include(S => S.Trainer)
                .Include(S => S.Category)
                .FirstOrDefault(S => S.Id == id);
        }
    }
}
