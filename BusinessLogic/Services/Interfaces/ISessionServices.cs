namespace BusinessLogic.Services.Interfaces
{
    public interface ISessionServices
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int id);
        bool CreateSession(CreateSessionViewModel createdSession);

        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);
        bool UpdateSession(UpdateSessionViewModel updatedSession, int sessionId);

        bool RemoveSession(int sessionId);
        SessionViewModel? GetSessionToDelete(int id);

        IEnumerable<TrainerSelectViewModel> GetTrainerForSession();
        IEnumerable<CategorySelectViewModel> GetCategoryForSession();

    }
}
