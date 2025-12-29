namespace BusinessLogic.ViewModels.MemberViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string? Photo { get; set; }= null!;
        public string Name { get; set; }  = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Gender { get; set; } = null!;

        //Details 
        public string? PlanName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? MembershipStratDate{ get; set; }
        public string? MembershipEndDate{ get; set; }
        public string? Address{ get; set; }
    }
}
