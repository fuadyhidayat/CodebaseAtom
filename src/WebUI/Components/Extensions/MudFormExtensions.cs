namespace Vioren.CodebaseAtom.WebUI.Components.Extensions;

public static class MudFormExtensions
{
    public static async Task<bool> IsValidAsync(this MudForm form)
    {
        await form.ValidateAsync();

        return form.IsValid;
    }
}
