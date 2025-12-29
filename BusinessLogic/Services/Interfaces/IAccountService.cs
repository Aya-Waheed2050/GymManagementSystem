namespace BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUser?> ValidateUserAsync(LoginViewModel loginVM);

        Task<IdentityResult> RegisterAsync(CreateNewUser model);
        Task<IEnumerable<UserViewModel>> GetUsersAsync();
        Task<bool> Delete(string userId);

    }
}
