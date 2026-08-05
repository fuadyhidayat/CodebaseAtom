namespace CodebaseAtom.WebUI.Components.Common.Errors;

public partial class ErrorViewer
{
    [Parameter]
    public required Exception? Exception { get; init; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public Variant Variant { get; set; } = Variant.Text;

    private string CssClass => string.Join(" ", "my-5", Class).Trim();
    private IReadOnlyList<string> _errorMessages = [];

    protected override void OnParametersSet()
    {
        if (Exception is null)
        {
            _errorMessages = [];

            return;
        }

        _errorMessages = GetAllErrorMessages(Exception);
    }

    private static List<string> GetAllErrorMessages(Exception exception)
    {
        var errorMessages = new List<string>();

        var current = exception;

        while (current is not null)
        {
            errorMessages.Add(current.Message);

            current = current.InnerException;
        }

        return errorMessages;
    }
}
