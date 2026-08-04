namespace Vioren.CodebaseExpress.Services.CurrentUser;

public interface ICurrentUserService
{
    public Task<CurrentUserModel?> GetCurrentUserAsync();
}
