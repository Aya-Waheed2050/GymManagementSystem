namespace BusinessLogic.Services.Classes
{
    public class SessionServices(IUnitOfWork _unitOfWork, IMapper _mapper) 
        : ISessionServices
    {


        public IEnumerable<SessionViewModel> GetAllSessions()
        {

            var Sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (!Sessions.Any())
                return [];
            var mappingSession = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(Sessions);
            foreach (var session in mappingSession)
            {
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookSlots(session.Id);
            }
            return mappingSession;
        }

        public SessionViewModel? GetSessionById(int id)
        {
            var Session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategoryById(id);
            if (Session == null)
                return null;

            var MappedSession = _mapper.Map<Session, SessionViewModel>(Session);
            MappedSession.AvailableSlots = MappedSession.Capacity - _unitOfWork.SessionRepository.GetCountOfBookSlots(MappedSession.Id);
            return MappedSession;
        }

        public bool CreateSession(CreateSessionViewModel createdSession)
        {
            try
            {
                if (!IsTrainerExists(createdSession.TrainerId) ||
                    !IsCategoryExists(createdSession.CategoryId) ||
                    !IsDateTimeValid(createdSession.StartDate, createdSession.EndDate) ||
                    !IsSessionCapacityValid(createdSession.Capacity))
                    return false;
                if (createdSession.Capacity > 25 || createdSession.Capacity < 1) 
                    return false;

                var Session = _mapper.Map<Session>(createdSession);
                _unitOfWork.GetRepository<Session>().Add(Session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create Session Failed : {ex}");
                return false;
            }
        }

        public UpdateSessionViewModel? GetSessionToUpdate(int sessionId)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);

            if (!IsSessionAvailableToUpdate(session!)) 
                return null;

            return _mapper.Map<UpdateSessionViewModel>(session);
        }

        public bool UpdateSession(UpdateSessionViewModel updatedSession, int sessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(sessionId);
                if (!IsSessionAvailableToUpdate(Session!)) 
                    return false;
                if (!IsTrainerExists(updatedSession.TrainerId)) 
                    return false;
                if (!IsDateTimeValid(updatedSession.StartDate, updatedSession.EndDate)) 
                    return false;

                _mapper.Map(updatedSession, Session);
                Session!.UpdatedAt = DateTime.Now;
                _unitOfWork.SessionRepository.Update(Session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Session Failed: {ex}");
                return false;
            }
        }

        public bool RemoveSession(int sessionId)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Session>();

                var Session = repo.GetById(sessionId);
                if (!IsSessionAvailableToDelete(Session!))
                    return false;

                _unitOfWork.SessionRepository.Delete(Session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete Session Failed : {ex}");
                return false;
            }
        }


        public IEnumerable<TrainerSelectViewModel> GetTrainerForSession()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll() ?? [];

            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(Trainers);
        }

        public IEnumerable<CategorySelectViewModel> GetCategoryForSession()
        {
            var Categories = _unitOfWork.GetRepository<Category>().GetAll() ?? [];
            return _mapper.Map<IEnumerable<CategorySelectViewModel>>(Categories);
        }

        public SessionViewModel? GetSessionToDelete(int id)
        {
            var session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategoryById(id);
            if (session is null)
                return null;

            if (!IsSessionAvailableToDelete(session)) 
                return null;

            return _mapper.Map<SessionViewModel>(session);
        }


        #region Helper Methods

        private bool IsTrainerExists(int trainerId) 
            => _unitOfWork.GetRepository<Trainer>().GetById(trainerId) is not null;
        

        private bool IsCategoryExists(int categoryId)
            => _unitOfWork.GetRepository<Category>().GetById(categoryId) is not null;

        private bool IsDateTimeValid(DateTime start, DateTime end)
            => start < end;
        private bool IsSessionCapacityValid(int capacity)
            => capacity >= 0 && capacity <= 25;
        
        private bool IsSessionAvailableToUpdate(Session session)
        {
            return session.StartDate > DateTime.Now &&
               _unitOfWork.SessionRepository.GetCountOfBookSlots(session.Id) == 0;
        }
        private bool IsSessionAvailableToDelete(Session session)
        {
            return session.EndDate < DateTime.Now &&
            _unitOfWork.SessionRepository.GetCountOfBookSlots(session.Id) == 0;
        }


        #endregion
    }
}
