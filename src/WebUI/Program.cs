using ApexCharts;
using MudBlazor.Services;
using Vioren.CodebaseAtom.WebUI.Components;
using Vioren.CodebaseAtom.WebUI.Infrastructure;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity.Extensions;
using Vioren.CodebaseAtom.WebUI.Logics;

var builder = WebApplication.CreateBuilder(args);
builder.AddInfrastructure();
builder.AddLogics();
builder.Services.AddMudServices();
builder.Services.AddApexCharts();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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
