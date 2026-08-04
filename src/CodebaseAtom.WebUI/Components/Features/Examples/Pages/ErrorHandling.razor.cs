namespace CodebaseAtom.WebUI.Components.Features.Examples.Pages;

public partial class ErrorHandling
{
    private const int _maximumCount = 5;
    private static readonly string _errorMessage = $"Counter exceeded the limit of {_maximumCount} clicks.";
    private int _currentCountWithErrorViewer;
    private int _currentCountWithoutErrorViewer;

    protected override void OnInitialized()
    {
        LoadBreadcrumbs();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ExamplesBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(ExamplesDisplayTextFor.ErrorHandling));
    }

    private void Reset()
    {
        _currentCountWithErrorViewer = 0;
        _currentCountWithoutErrorViewer = 0;

        ExceptionBase = null;
    }

    private void IncrementCountWithErrorViewer()
    {
        try
        {
            _currentCountWithErrorViewer++;

            if (_currentCountWithErrorViewer > _maximumCount)
            {
                throw new InvalidOperationException(_errorMessage);
            }
        }
        catch (Exception exception)
        {
            ExceptionBase = exception;
        }
    }

    private void IncrementCountWithoutErrorViewer()
    {
        _currentCountWithoutErrorViewer++;

        if (_currentCountWithoutErrorViewer > _maximumCount)
        {
            throw new InvalidOperationException(_errorMessage);
        }
    }
}
