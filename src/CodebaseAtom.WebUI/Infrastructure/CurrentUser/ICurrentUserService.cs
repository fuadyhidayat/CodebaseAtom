namespace CodebaseAtom.WebUI.Infrastructure.CurrentUser;

public interface ICurrentUserService
{
    public Task<CurrentUserModel?> GetCurrentUserAsync();
}
