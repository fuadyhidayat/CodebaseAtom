using System.Security.Claims;
using ApexCharts;
using CodebaseAtom.WebUI.Common.Models;
using MudBlazor.Services;

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
            var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext is null)
            {
                return null;
            }

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                return null;
            }

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new InvalidOperationException($"Invalid user ID format: {userIdClaim.Value}");
            }

            var usernameClaim = httpContext.User.FindFirst(ClaimTypes.Name);

            return new CurrentUser
            {
                UserId = userId,
                Username = usernameClaim is null ? "unknown" : usernameClaim.Value,
                Roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()
            };
        });

        return builder;
    }
}
