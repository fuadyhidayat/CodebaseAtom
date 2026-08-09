using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile.Components;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile;

public partial class Index
{
    [Inject]
    public required CurrentUserService CurrentUserService { get; init; }

    //[Inject]
    //public required CurrentUserState CurrentUserState { get; init; }

    [Inject]
    public required IDialogService DialogService { get; init; }

    [Inject]
    public required UserManager<ApplicationUser> UserManager { get; init; }

    private ApplicationUser _applicationUser = default!;
    private IReadOnlyCollection<string> _roles = [];

    protected override async Task OnInitializedAsync()
    {
        LoadBreadcrumbs();
        await LoadApplicationUserAsync();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(UIDisplayTextFor.Profile));
    }

    private async Task LoadApplicationUserAsync()
    {
        try
        {
            var currentUser = await CurrentUserService.GetCurrentUserAsync();

            if (currentUser is not null)
            {
                var user = await UserManager.FindByIdAsync(currentUser.UserId.ToString())
                    ?? throw new InvalidOperationException($"User with ID '{currentUser.UserId}' not found.");

                _applicationUser = user;
                _roles = currentUser.Roles;
            }
        }
        catch (Exception exception)
        {
            ExceptionBase = exception;
        }
    }

    private async Task ShowDialogEditProfile()
    {
        var parameters = new DialogParameters<DialogEditProfile>
        {
            { x => x.ApplicationUser, _applicationUser }
        };

        var dialog = await DialogService.ShowAsync<DialogEditProfile>($"{UIDisplayTextFor.Edit} {UIDisplayTextFor.Profile}", parameters);

        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadApplicationUserAsync();
        }
    }

    private async Task ShowDialogChangePassword()
    {
        var parameters = new DialogParameters<DialogChangePassword>
        {
            { x => x.ApplicationUser, _applicationUser }
        };

        _ = await DialogService.ShowAsync<DialogChangePassword>($"{UIDisplayTextFor.Change} {DomainDisplayTextFor.Password}", parameters);
    }
}
