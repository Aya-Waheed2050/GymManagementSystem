namespace DataAccess.Repositories.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionsWithTrainerAndCategory();
        int GetCountOfBookSlots(int SessionId);
        Session? GetSessionWithTrainerAndCategoryById(int id);
    }
}
