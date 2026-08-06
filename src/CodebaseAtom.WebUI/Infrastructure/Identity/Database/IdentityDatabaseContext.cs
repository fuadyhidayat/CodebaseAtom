using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CodebaseAtom.WebUI.Infrastructure.Identity.Database;

public sealed class IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public const string SchemaName = "Identity";

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        _ = builder.HasDefaultSchema(SchemaName);
        _ = builder.Entity<ApplicationRole>(x => x.ToTable("Roles"));
        _ = builder.Entity<IdentityRoleClaim<Guid>>(x => x.ToTable("RoleClaims"));

        _ = builder.Entity<ApplicationUser>(x => x.ToTable("Users"));
        _ = builder.Entity<ApplicationUser>(entity => entity.Property(u => u.UserName).HasMaxLength(MaximumLengthFor.Username).IsRequired());
        _ = builder.Entity<ApplicationUser>(entity => entity.Property(u => u.NormalizedUserName).HasMaxLength(MaximumLengthFor.Username).IsRequired());
        _ = builder.Entity<ApplicationUser>(entity => entity.Property(u => u.Email).HasMaxLength(MaximumLengthFor.Email).IsRequired());
        _ = builder.Entity<ApplicationUser>(entity => entity.Property(u => u.NormalizedEmail).HasMaxLength(MaximumLengthFor.Email).IsRequired());
        _ = builder.Entity<ApplicationUser>(entity => entity.Property(u => u.DisplayName).HasMaxLength(MaximumLengthFor.Name).IsRequired());

        _ = builder.Entity<IdentityUserClaim<Guid>>(x => x.ToTable("UserClaims"));
        _ = builder.Entity<IdentityUserLogin<Guid>>(x => x.ToTable("UserLogins"));
        _ = builder.Entity<IdentityUserToken<Guid>>(x => x.ToTable("UserTokens"));
        _ = builder.Entity<IdentityUserRole<Guid>>(x => x.ToTable("UserRoles"));
        _ = builder.Entity<IdentityUserPasskey<Guid>>(x => x.ToTable("UserPasskeys"));
    }
}
