namespace Vioren.CodebaseAtom.WebUI.Common.Validators;

public abstract class AbstractValidatorBase<T> : AbstractValidator<T>
    where T : class
{
    /// <summary>
    /// Helper function for the Validation property on MudForm or MudBlazor controls.
    /// Example: Validation="@_validator.ValidateValue"
    /// </summary>
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        // Safety check if model is null or the type does not match T
        if (model is not T instance)
        {
            return Array.Empty<string>();
        }

        var validationContext = ValidationContext<T>.CreateWithOptions(instance, options => options.IncludeProperties(propertyName));

        var result = await ValidateAsync(validationContext);

        if (result.IsValid)
        {
            return Array.Empty<string>();
        }

        return result.Errors.Select(x => x.ErrorMessage);
    };
}
