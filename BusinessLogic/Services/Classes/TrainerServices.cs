namespace BusinessLogic.Services.Classes
{
    public class TrainerServices(IUnitOfWork _unitOfWork , IMapper _mapper) 
        : ITrainerServices
    {
      

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();

            return _mapper.Map<IEnumerable<TrainerViewModel>>(Trainers);
        }

        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            try
            {
                if (IsEmailExist(createTrainer.Email) || IsPhoneExist(createTrainer.Phone)) 
                    return false;

                var trainer = _mapper.Map<Trainer>(createTrainer);
                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer is null) 
                return null;

            return _mapper.Map<TrainerViewModel>(trainer);
        }

        public UpdateTrainerViewModel? GetTrainerToUpdate(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer is null)
                return null;

            return _mapper.Map<UpdateTrainerViewModel>(trainer);
        }

        public bool UpdateTrainerDetails(int trainerId, UpdateTrainerViewModel updatedTrainer)
        {
            try
            {
                var emailExists = _unitOfWork.GetRepository<Trainer>()
                    .GetAll(X => X.Email == updatedTrainer.Email && X.Id != trainerId);

                var phoneExists = _unitOfWork.GetRepository<Trainer>()
                    .GetAll(X => X.Phone == updatedTrainer.Phone && X.Id != trainerId);

                if (emailExists.Any() || phoneExists.Any())
                    return false;

                var TrainerRepo = _unitOfWork.GetRepository<Trainer>();
                var TrainerToUpdate = TrainerRepo.GetById(trainerId);
                if (TrainerToUpdate is null) 
                    return false;

                _mapper.Map(updatedTrainer, TrainerToUpdate);
                TrainerRepo.Update(TrainerToUpdate);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveTrainer(int trainerId)
        {
            var TrainerRepo = _unitOfWork.GetRepository<Trainer>();
            var SessionRepo = _unitOfWork.GetRepository<Session>();
            var trainer = TrainerRepo.GetById(trainerId);
            if (trainer is null) return false;

            var HasFutureSessions = SessionRepo
                .GetAll(X => X.TrainerId == trainerId && X.StartDate > DateTime.Now)
                .Any();
            if (HasFutureSessions) return false;

            TrainerRepo.Delete(trainer);
            return _unitOfWork.SaveChanges() > 0;
        }

        #region Helper Method

        public bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(X => X.Email == email).Any();
        }

        public bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(X => X.Phone == phone).Any();
        }

        #endregion

    }
}
