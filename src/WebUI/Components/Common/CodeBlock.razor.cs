namespace Vioren.CodebaseAtom.WebUI.Components.Common;

public partial class CodeBlock
{
    [Parameter]
    public RoundedMode RoundedMode { get; set; } = RoundedMode.Full;

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private const string _defaultCssClass = "mud-background-gray pa-5 border mud-border-lines-default";
    private const string _cssClassForFull = $"{_defaultCssClass} rounded my-4";
    private const string _cssClassForBottom = $"{_defaultCssClass} rounded-b";

    private string CssClass => RoundedMode is RoundedMode.Full
        ? $"{_cssClassForFull} {Class}"
        : $"{_cssClassForBottom} {Class}";
}

public enum RoundedMode
{
    Full,
    Bottom
}
