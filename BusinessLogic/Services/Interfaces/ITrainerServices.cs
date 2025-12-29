namespace BusinessLogic.Services.Interfaces
{
    public interface ITrainerServices
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();

        bool CreateTrainer(CreateTrainerViewModel createTrainer);

        TrainerViewModel? GetTrainerDetails(int trainerId);

        UpdateTrainerViewModel? GetTrainerToUpdate(int trainerId);

        bool UpdateTrainerDetails(int trainerId, UpdateTrainerViewModel updatedTrainer);

        bool RemoveTrainer(int trainerId);
    }
}
