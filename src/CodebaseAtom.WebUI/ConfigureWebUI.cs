using ApexCharts;
using MudBlazor.Services;
using CodebaseAtom.WebUI.Infrastructure.CurrentUser;

namespace CodebaseAtom.WebUI;

public static class ConfigureWebUI
{
    public static WebApplicationBuilder AddWebUIServices(this WebApplicationBuilder builder)
    {
        _ = builder.Services.AddMudServices();
        _ = builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        _ = builder.Services.AddApexCharts(apexChartsServiceOptions =>
        {
            apexChartsServiceOptions.GlobalOptions = new ApexChartBaseOptions
            {
                Theme = new Theme { Palette = PaletteType.Palette6 }
            };
        });

        _ = builder.Services.AddCascadingValue(serviceProvider =>
        {
            var currentUserService = serviceProvider.GetRequiredService<ICurrentUserService>();

            return currentUserService.GetCurrentUserAsync().GetAwaiter().GetResult();
        });

        return builder;
    }
}
