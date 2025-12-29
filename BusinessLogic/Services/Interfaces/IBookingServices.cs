namespace BusinessLogic.Services.Interfaces
{
    public interface IBookingServices
    {
        IEnumerable<SessionViewModel> GetAllSessionsWithTrainerAndCategory();
        IEnumerable<MemberForSessionViewModel> GetAllMembersForSession(int id);
        bool CreateBooking(CreateBookingViewModel model);
        IEnumerable<MemberForSelectListViewModel> GetMemberForDropdown(int id);
        bool MemberAttended(MemberAttendOrCancelViewModel model);
        bool CancelBooking(MemberAttendOrCancelViewModel model);
    }
}