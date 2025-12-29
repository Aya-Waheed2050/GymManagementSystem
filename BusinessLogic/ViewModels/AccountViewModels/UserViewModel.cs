namespace BusinessLogic.ViewModels.AccountViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;

    }
}
