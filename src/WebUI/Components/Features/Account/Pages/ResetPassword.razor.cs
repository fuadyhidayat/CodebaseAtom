using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Account.Pages;

public partial class ResetPassword
{
    [Inject]
    public required UserManager<ApplicationUser> UserManager { get; init; }

    private bool _succeeded;
    private Exception? _exceptionCode;
    private Exception? _exception;

    private InputModel Input { get; set; } = new();

    [SupplyParameterFromQuery]
    private string? Code { get; set; }

    protected override void OnInitialized()
    {
        if (Code is null)
        {
            _exceptionCode = new InvalidOperationException("A code must be supplied for password reset.");

            return;
        }

        try
        {
            Input.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code));
        }
        catch (Exception exception)
        {
            _exceptionCode = exception;
        }
    }

    private async Task OnValidSubmitAsync()
    {
        var user = await UserManager.FindByNameAsync(Input.Username);

        if (user is null)
        {
            // Don't reveal that the user does not exist
            _succeeded = true;

            return;
        }

        var result = await UserManager.ResetPasswordAsync(user, Input.Code, Input.Password);

        if (result.Succeeded)
        {
            _succeeded = true;

            return;
        }

        var lastError = result.Errors.Last();
        _exception = new InvalidOperationException(lastError.Description);
    }

    private sealed record InputModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(MaximumLengthFor.Password, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = MinimumLengthFor.Password)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string Code { get; set; } = string.Empty;
    }
}
