using Microsoft.EntityFrameworkCore.Diagnostics;
using CodebaseAtom.WebUI.Infrastructure.CurrentUser;

namespace CodebaseAtom.WebUI.Infrastructure.Database.Interceptors;

public sealed class AuditingSaveChangesInterceptor(ICurrentUserService currentUserService)
    : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context!;
        var currentUser = await currentUserService.GetCurrentUserAsync();
        var now = DateTimeOffset.Now;
        var userId = currentUser?.UserId ?? Guid.Empty;

        foreach (var entry in context.ChangeTracker.Entries<CreatableEntity>())
        {
            if (entry.State is EntityState.Added && entry.Entity is CreatableEntity creatable)
            {
                creatable.Created = now;
                creatable.CreatedBy = userId;
            }
            else if (entry.State is EntityState.Modified && entry.Entity is ModifiableEntity modifiable)
            {
                modifiable.Modified = now;
                modifiable.ModifiedBy = userId;
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
