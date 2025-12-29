namespace BusinessLogic.Services.Classes
{
    public class AnalyticsServices(IUnitOfWork _unitOfWork) : IAnalyticsServices
    {

        public AnalyticsViewModel GetAnalyticsData()
        {
            var Sessions = _unitOfWork.GetRepository<Session>().GetAll();
            return new AnalyticsViewModel()
            {
                ActiveMembers = _unitOfWork.GetRepository<Membership>().GetAll(X => X.Status == "Active").Count(),
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = Sessions.Count(X => X.StartDate > DateTime.Now),
                OngoingSessions = Sessions.Count(X => X.StartDate <= DateTime.Now && X.EndDate >= DateTime.Now),
                CompletedSessions = Sessions.Count(X => X.EndDate < DateTime.Now)
            };
        }
    }
}
