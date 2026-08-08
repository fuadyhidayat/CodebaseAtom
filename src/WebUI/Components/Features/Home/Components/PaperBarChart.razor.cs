using ApexCharts;
using Vioren.CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Home.Components;

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
