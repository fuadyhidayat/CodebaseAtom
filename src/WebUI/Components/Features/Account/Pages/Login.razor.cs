using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages;

public partial class Login
{
    [Inject]
    public required SignInManager<ApplicationUser> SignInManager { get; init; }

    [CascadingParameter]
    private HttpContext HttpContext { get; set; } = default!;

    [SupplyParameterFromForm]
    private InputModel Input { get; set; } = default!;

    [SupplyParameterFromQuery]
    private string? ReturnUrl { get; set; }

    private EditContext _editContext = default!;
    private string _errorMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        Input ??= new();

        _editContext = new EditContext(Input);

        if (HttpMethods.IsGet(HttpContext.Request.Method))
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }
    }

    public async Task HandleSubmit()
    {
        if (!_editContext.Validate())
        {
            return;
        }

        var result = await SignInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            RedirectTo(ReturnUrl);
        }
        else if (result.IsLockedOut)
        {
            RedirectTo(AccountRouteFor.Lockout);
        }
        else
        {
            _errorMessage = "Invalid Username or Password.";
        }
    }

    private void RedirectTo(string? uri, bool forceLoad = false)
    {
        uri ??= "";

        // Prevent open redirects.
        if (!Uri.IsWellFormedUriString(uri, UriKind.Relative))
        {
            uri = NavigationManager.ToBaseRelativePath(uri);
        }

        NavigationManager.NavigateTo(uri, forceLoad);
    }

    private sealed record InputModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
