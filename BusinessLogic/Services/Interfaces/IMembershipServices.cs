namespace BusinessLogic.Services.Interfaces
{
    public interface IMembershipServices
    {
        IEnumerable<MembershipViewModel> GetAllMemberships();
        IEnumerable<MemberForSelectListViewModel> GetMembersForDropdown();
        IEnumerable<PlanForSelectListViewModel> GetPlansForDropdown();
        bool CreateMembership(CreateMembershipViewModel model);
        bool DeleteMembership(int memberId);
    }
}
