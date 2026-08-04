using ApexCharts;
using CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;

namespace CodebaseAtom.WebUI.Components.Features.Home.Components;

public partial class PaperDonutChart
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
        },
        DataLabels = new DataLabels
        {
            Enabled = true,
            Formatter = "function (val, opts) { return opts.w.config.series[opts.seriesIndex]; }",
            Style = new DataLabelsStyle
            {
                FontSize = "14px",
                FontWeight = "bold",
                Colors = new List<string> { "#D807B8" }
            }
        },
        PlotOptions = new PlotOptions
        {
            Pie = new PlotOptionsPie
            {
                Donut = new PlotOptionsDonut
                {
                    Labels = new DonutLabels
                    {
                        Total = new DonutLabelTotal
                        {
                            FontSize = "24px",
                            Color = "#D807B8",
                            Formatter = @"function (w) {return w.globals.seriesTotals.reduce((a, b) => { return (a + b) }, 0)}"
                        }
                    }
                }
            }
        },
        Legend = new Legend
        {
            Position = LegendPosition.Bottom,
            HorizontalAlign = ApexCharts.Align.Right
        }
    };
}
