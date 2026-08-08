using ApexCharts;
using MudBlazor.Services;

namespace Vioren.CodebaseAtom.WebUI;

public static class ConfigureWebUI
{
    public static WebApplicationBuilder AddWebUIServices(this WebApplicationBuilder builder)
    {
        //_ = builder.Services.AddCurrentUserCascadingValue();
        _ = builder.Services.AddMudServices();
        _ = builder.Services.AddApexCharts();
        _ = builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        return builder;
    }

    //private static IServiceCollection AddCurrentUserCascadingValue(this IServiceCollection services)
    //{
    //    _ = services.AddCascadingValue(serviceProvider =>
    //    {
    //        var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    //        var httpContext = httpContextAccessor.HttpContext;

    //        if (httpContext is null)
    //        {
    //            return null;
    //        }

    //        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

    //        if (userIdClaim is null)
    //        {
    //            return null;
    //        }

    //        if (!Guid.TryParse(userIdClaim.Value, out var userId))
    //        {
    //            throw new InvalidOperationException($"Invalid user ID format: {userIdClaim.Value}");
    //        }

    //        return new CurrentUser
    //        {
    //            UserId = userId,
    //            Username = httpContext.User.FindFirst(ClaimTypes.Name)?.Value ?? "unknown",
    //            Roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()
    //        };
    //    });

    //    return services;
    //}
}
