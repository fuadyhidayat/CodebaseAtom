using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vioren.CodebaseExpress.Domain.Common.Statics;
using Vioren.CodebaseExpress.Services.Identity;

namespace Vioren.CodebaseExpress.Infrastructure.Identity.Database;

public sealed class IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public const string SchemaName = "Identity";
    public const string TableRoles = "Roles";
    public const string TableRoleClaims = "RoleClaims";
    public const string TableUsers = "Users";
    public const string TableUserClaims = "UserClaims";
    public const string TableUserLogins = "UserLogins";
    public const string TableUserTokens = "UserTokens";
    public const string TableUserRoles = "UserRoles";
    public const string TableUserPasskeys = "UserPasskeys";

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        _ = builder.HasDefaultSchema(SchemaName);
        _ = builder.Entity<ApplicationRole>(x => x.ToTable(TableRoles));
        _ = builder.Entity<IdentityRoleClaim<Guid>>(x => x.ToTable(TableRoleClaims));

        _ = builder.Entity<ApplicationUser>(x => x.ToTable(TableUsers));
        _ = builder.Entity<ApplicationUser>(entity => entity.Property(u => u.UserName).HasMaxLength(MaximumLengthFor.Username).IsRequired());
        _ = builder.Entity<ApplicationUser>(entity => entity.Property(u => u.NormalizedUserName).HasMaxLength(MaximumLengthFor.Username).IsRequired());
        _ = builder.Entity<ApplicationUser>(entity => entity.Property(u => u.Email).HasMaxLength(MaximumLengthFor.Email).IsRequired());
        _ = builder.Entity<ApplicationUser>(entity => entity.Property(u => u.NormalizedEmail).HasMaxLength(MaximumLengthFor.Email).IsRequired());
        _ = builder.Entity<ApplicationUser>(entity => entity.Property(u => u.DisplayName).HasMaxLength(MaximumLengthFor.Name).IsRequired());

        _ = builder.Entity<IdentityUserClaim<Guid>>(x => x.ToTable(TableUserClaims));
        _ = builder.Entity<IdentityUserLogin<Guid>>(x => x.ToTable(TableUserLogins));
        _ = builder.Entity<IdentityUserToken<Guid>>(x => x.ToTable(TableUserTokens));
        _ = builder.Entity<IdentityUserRole<Guid>>(x => x.ToTable(TableUserRoles));
        _ = builder.Entity<IdentityUserPasskey<Guid>>(x => x.ToTable(TableUserPasskeys));

    }
}
