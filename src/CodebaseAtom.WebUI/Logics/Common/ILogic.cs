namespace CodebaseAtom.WebUI.Logics.Common;

/// <summary>
/// Defines a logic handler that processes an input and returns an output.
/// </summary>
public interface ILogic<TInput, TOutput>
{
    public Task<TOutput> Handle(TInput input, CancellationToken cancellationToken = default);
}
