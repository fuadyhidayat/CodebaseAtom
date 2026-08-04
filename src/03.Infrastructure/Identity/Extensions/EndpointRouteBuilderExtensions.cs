using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Vioren.CodebaseExpress.Services.Identity;

namespace Vioren.CodebaseExpress.Infrastructure.Identity.Extensions;

public static partial class EndpointRouteBuilderExtensions
{
    public static IEndpointConventionBuilder MapIdentityEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var routeGroupAccount = endpoints.MapGroup("/Account");

        _ = routeGroupAccount.MapPost("/Logout", async (
            ClaimsPrincipal user,
            [FromServices] SignInManager<ApplicationUser> signInManager,
            [FromForm] string returnUrl) =>
        {
            await signInManager.SignOutAsync();

            return TypedResults.LocalRedirect($"~/{returnUrl}");
        });

        return routeGroupAccount;
    }
}
