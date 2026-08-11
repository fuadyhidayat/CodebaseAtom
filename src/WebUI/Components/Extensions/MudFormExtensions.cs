using Vioren.CodebaseAtom.WebUI.Common.Exceptions;

namespace Vioren.CodebaseAtom.WebUI.Components.Extensions;

public static class MudFormExtensions
{
    public static async Task RunValidation(this MudForm form)
    {
        await form.ValidateAsync();

        if (!form.IsValid)
        {
            throw new FormValidationException(form.Errors);
        }
    }
}
