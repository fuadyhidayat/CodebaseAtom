using Microsoft.AspNetCore.Components.Routing;
using CodebaseAtom.WebUI.Infrastructure.CurrentUser;

namespace CodebaseAtom.WebUI.Components.Layouts.Components;

public sealed partial class AccountInfo
{
    [CascadingParameter]
    private CurrentUserModel? CurrentUser { get; set; } = default!;

    private string? _currentUrl;
    private string _loginRoute = AccountRouteFor.Login();
    private string _username = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        SetupRoutes(NavigationManager.Uri);
        NavigationManager.LocationChanged += OnLocationChanged;

        if (CurrentUser is not null)
        {
            _username = CurrentUser.Username;
        }
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
    }
}
