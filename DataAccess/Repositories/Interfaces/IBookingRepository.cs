namespace DataAccess.Repositories.Interfaces
{
    public interface IBookingRepository : IGenericRepository<MemberSession>
    {
        IEnumerable<MemberSession> GetSessionById(int sessionId);
    }
}
