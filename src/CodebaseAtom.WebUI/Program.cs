using CodebaseAtom.WebUI;
using CodebaseAtom.WebUI.Components;
using CodebaseAtom.WebUI.Infrastructure;
using CodebaseAtom.WebUI.Infrastructure.Identity;
using CodebaseAtom.WebUI.Infrastructure.Identity.Extensions;
using CodebaseAtom.WebUI.Logics;

var builder = WebApplication.CreateBuilder(args);
builder.AddInfrastructure();
builder.AddLogics();
builder.AddWebUIServices();

var app = builder.Build();
await app.InitializeIdentityDatabase();
await app.InitializeDatabase();

app.UseExceptionHandler($"/{HomeRouteFor.Error}", createScopeForErrors: true);
app.UseHsts();
app.UseStatusCodePagesWithReExecute($"/{HomeRouteFor.NotFound}", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapIdentityEndpoints();
await app.RunAsync();
