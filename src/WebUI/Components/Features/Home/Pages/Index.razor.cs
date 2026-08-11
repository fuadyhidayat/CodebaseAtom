using Vioren.CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Home.Pages;

public partial class Index
{
    [Inject]
    public required GetStatisticLogic GetStatisticLogic { get; set; }

    private GetStatisticOutput _item = default!;

    protected override async Task OnInitializedAsync()
    {
        LoadBreadcrumbs();
        await LoadItems();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(UIDisplayTextFor.Home));
    }

    private async Task LoadItems()
    {
        try
        {
            IsLoadingBase = true;

            _item = await GetStatisticLogic.Handle();
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
