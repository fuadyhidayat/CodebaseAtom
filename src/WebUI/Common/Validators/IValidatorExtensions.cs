namespace Vioren.CodebaseAtom.WebUI.Common.Validators;

public static class IValidatorExtensions
{
    public static async Task ValidateInputAsync<T>(this IValidator<T> validator, T input, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(input, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ModelValidationException(validationResult.Errors);
        }
    }
}
