using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vioren.CodebaseExpress.Domain.Common.Statics;
using Vioren.CodebaseExpress.Infrastructure.Database.Statics;

namespace Vioren.CodebaseExpress.Infrastructure.Database.Configurations;

public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        _ = builder.ToTable(nameof(DatabaseService.Projects));

        _ = builder.Property(entity => entity.Title)
            .HasColumnType(ColumnTypeFor.Nvarchar(MaximumLengthFor.Title));

        _ = builder.Property(entity => entity.Description)
            .HasColumnType(ColumnTypeFor.Nvarchar(MaximumLengthFor.Description));
    }
}
