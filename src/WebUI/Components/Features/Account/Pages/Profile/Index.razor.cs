using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile.Components;
using Vioren.CodebaseAtom.WebUI.Infrastructure.CurrentUser;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages.Profile;

public partial class Index
{
    [Inject]
    public required IDialogService DialogService { get; init; }

    [Inject]
    public required UserManager<ApplicationUser> UserManager { get; init; }

    [CascadingParameter]
    private CurrentUserModel? CurrentUser { get; set; }

    private ApplicationUser _applicationUser = default!;
    private IReadOnlyCollection<string> _roles = [];

    protected override void OnInitialized()
    {
        LoadBreadcrumbs();
    }

    protected override async Task OnParametersSetAsync()
    {
        // Guard: tunggu cascading user terisi, jangan redirect di pass awal
        if (CurrentUser is null)
        {
            return;
        }

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
            Console.WriteLine("---- Loading application user...");
            Console.WriteLine($"---- CurrentUser: {CurrentUser?.UserId}, Roles: {string.Join(", ", CurrentUser?.Roles ?? Array.Empty<string>())}");

            if (CurrentUser is null)
            {
                NavigationManager.NavigateTo(AccountRouteFor.Login());

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
