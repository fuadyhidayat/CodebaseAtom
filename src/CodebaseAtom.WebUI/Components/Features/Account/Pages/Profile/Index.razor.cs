using CodebaseAtom.WebUI.Common.Models;
using CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile.Components;
using CodebaseAtom.WebUI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile;

public partial class Index
{
    [Inject]
    public required IDialogService DialogService { get; init; }

    [Inject]
    public required UserManager<ApplicationUser> UserManager { get; init; }

    [CascadingParameter]
    private CurrentUser? CurrentUser { get; set; }

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
            if (CurrentUser is null)
            {
                NavigationManager.NavigateTo(AccountRouteFor.Login(), forceLoad: true);

                return;
            }

            var user = await UserManager.FindByIdAsync(CurrentUser.UserId.ToString())
                ?? throw new InvalidOperationException($"User with ID '{CurrentUser.UserId}' not found.");

            _applicationUser = user;
            _roles = CurrentUser.Roles;
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

        var dialog = await DialogService.ShowAsync<DialogEditProfile>($"{UIDisplayTextFor.Edit} {DomainDisplayTextFor.Email}", parameters);

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
