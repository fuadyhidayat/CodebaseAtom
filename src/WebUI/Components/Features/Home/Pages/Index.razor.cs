using Vioren.CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Home.Pages;

public partial class Index
{
    [Inject]
    public required GetStatisticLogic GetStatisticLogic { get; set; }

    private GetStatisticOutput _statistic = default!;

    protected override async Task OnInitializedAsync()
    {
        LoadBreadcrumbs();
        await LoadStatistic();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(UIDisplayTextFor.Home));
    }

    private async Task LoadStatistic()
    {
        try
        {
            IsLoadingBase = true;
            ExceptionBase = null;

            _statistic = await GetStatisticLogic.Handle();
        }
        catch (Exception exception)
        {
            ExceptionBase = exception;
        }
        finally
        {
            IsLoadingBase = false;
        }
    }
}
