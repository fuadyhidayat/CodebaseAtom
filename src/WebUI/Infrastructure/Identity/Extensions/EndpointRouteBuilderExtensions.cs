using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Identity.Extensions;

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
