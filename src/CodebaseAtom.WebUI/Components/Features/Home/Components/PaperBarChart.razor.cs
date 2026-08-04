using ApexCharts;
using CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

namespace CodebaseAtom.WebUI.Components.Features.Home.Components;

public partial class PaperBarChart
{
    [Parameter, EditorRequired]
    public IReadOnlyList<ProjectDto> Projects { get; set; }

    private readonly ApexChartOptions<ProjectDto> _options = new()
    {
        Chart = new Chart
        {
            Toolbar = new Toolbar
            {
                Show = false
            }
        }
    };
}
