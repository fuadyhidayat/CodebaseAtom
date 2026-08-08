using Microsoft.AspNetCore.Components.Routing;

namespace Vioren.CodebaseAtom.WebUI.Components.Layouts.Components;

public sealed partial class AccountInfo
{
    [Inject]
    public required CurrentUserService CurrentUserService { get; init; }

    [Inject]
    public required CurrentUserState CurrentUserState { get; init; }

    private string? _currentUrl;
    private string _loginRoute = AccountRouteFor.Login();
    private CurrentUserModel? _currentUser;

    protected override async Task OnInitializedAsync()
    {
        _currentUser = await CurrentUserService.GetCurrentUserAsync();

        CurrentUserState.OnChanged += HandleCurrentUserChanged;
    }

    private void HandleCurrentUserChanged(object? sender, EventArgs e)
    {
        _currentUser = CurrentUserState.CurrentUser;
        _ = InvokeAsync(StateHasChanged);
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        SetupRoutes(e.Location);
    }

    private void SetupRoutes(string currentUrl)
    {
        var uri = new Uri(currentUrl);

        _currentUrl = NavigationManager.ToBaseRelativePath(currentUrl);
        _loginRoute = AccountRouteFor.Login(uri.AbsolutePath);

        StateHasChanged();
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
        CurrentUserState.OnChanged -= HandleCurrentUserChanged;
    }
}
