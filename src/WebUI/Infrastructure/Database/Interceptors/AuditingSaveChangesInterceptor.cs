using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Database.Interceptors;

public sealed class AuditingSaveChangesInterceptor(CurrentUserService currentUserService)
    : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context!;
        var now = DateTimeOffset.Now;
        var currentUser = await currentUserService.GetCurrentUserAsync();

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State is EntityState.Added)
            {
                entry.Entity.CreatedAt = now;

                if (currentUser is not null)
                {
                    entry.Entity.CreatedBy = currentUser.UserId;
                }
            }
            else if (entry.State is EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;

                if (currentUser is not null)
                {
                    entry.Entity.UpdatedBy = currentUser.UserId;
                }
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
