using Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile.Components;
using Vioren.CodebaseAtom.WebUI.Logics.Users.GetCurrentUser;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile;

public partial class Index
{
    [Inject]
    public required CurrentUserService CurrentUserService { get; init; }

    [Inject]
    public required IDialogService DialogService { get; init; }

    [Inject]
    public required GetCurrentUserLogic GetCurrentUserLogic { get; init; }

    private UserDto _user = default!;

    protected override async Task OnInitializedAsync()
    {
        LoadBreadcrumbs();
        await LoadUser();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(UIDisplayTextFor.Profile));
    }

    private async Task LoadUser()
    {
        try
        {
            var output = await GetCurrentUserLogic.Handle();
            _user = output.User;
        }
        catch (Exception exception)
        {
            ExceptionBase = exception;
        }
    }

    private async Task ShowDialogEditProfile()
    {
        var model = new EditProfileModel
        {
            UserId = _user.Id,
            DisplayName = _user.DisplayName,
            NewEmail = _user.Email
        };

        var parameters = new DialogParameters<DialogEditProfile>
        {
            { x => x.Model, model }
        };

        var dialog = await DialogService.ShowAsync<DialogEditProfile>($"{UIDisplayTextFor.Edit} {UIDisplayTextFor.Profile}", parameters);

        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadUser();
        }
    }

    private async Task ShowDialogChangePassword()
    {
        var model = new ChangePasswordModel
        {
            UserId = _user.Id
        };

        var parameters = new DialogParameters<DialogChangePassword>
        {
            { x => x.Model, model }
        };

        _ = await DialogService.ShowAsync<DialogChangePassword>($"{UIDisplayTextFor.Change} {DomainDisplayTextFor.Password}", parameters);
    }
}
