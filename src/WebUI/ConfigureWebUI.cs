using ApexCharts;
using MudBlazor.Services;

namespace Vioren.CodebaseAtom.WebUI;

public static class ConfigureWebUI
{
    public static WebApplicationBuilder AddWebUIServices(this WebApplicationBuilder builder)
    {
        _ = builder.Services.AddMudServices();
        _ = builder.Services.AddApexCharts();
        _ = builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        return builder;
    }
}
