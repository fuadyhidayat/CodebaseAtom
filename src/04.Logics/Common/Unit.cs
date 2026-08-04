namespace Vioren.CodebaseExpress.Logics.Common;

/// <summary>
/// Represents the absence of a value. Used as a placeholder for logics with no input or no output.
/// </summary>
public sealed record Unit
{
    public static readonly Unit Value = new();
}
