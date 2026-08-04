using System.Security.Claims;
using ApexCharts;
using CodebaseAtom.WebUI.Common.Models;
using MudBlazor.Services;

namespace CodebaseAtom.WebUI;

public static class ConfigureWebUI
{
    public static WebApplicationBuilder AddWebUIServices(this WebApplicationBuilder builder)
    {
        _ = builder.Services.AddCurrentUserCascadingValue();
        _ = builder.Services.AddMudServices();
        _ = builder.Services.AddApexCharts();
        _ = builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        return builder;
    }

    private static IServiceCollection AddCurrentUserCascadingValue(this IServiceCollection services)
    {
        _ = services.AddCascadingValue(serviceProvider =>
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

        return services;
    }
}
