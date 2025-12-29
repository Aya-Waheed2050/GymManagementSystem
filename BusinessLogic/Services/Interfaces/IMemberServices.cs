namespace BusinessLogic.Services.Interfaces
{
    public interface IMemberServices
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember(CreateMemberViewModel createMember);
        MemberViewModel? GetMemberDetails(int MemberId);
        HealthViewModel? GetMemberHealthDetails(int MemberId);
        UpdateMemberViewModel? UpdateMemberById(int MemberId);
        bool UpdateMemberDetails(int MemberId, UpdateMemberViewModel updatedMember);
        bool DeleteMember(int MemberId);
    }
}
