namespace DataAccess.Repositories.Classes
{
    public class BookingRepository : GenericRepository<MemberSession>, IBookingRepository
    {
        private readonly GymSystemDbContext _dbContext;

        public BookingRepository(GymSystemDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MemberSession> GetSessionById(int sessionId)
        {
            return _dbContext.MemberSessions.Where(ms => ms.SessionId == sessionId)
                                            .Include(ms => ms.Member)
                                            .ToList();
        }
    }
}
