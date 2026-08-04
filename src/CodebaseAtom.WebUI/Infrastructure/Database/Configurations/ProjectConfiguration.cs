using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CodebaseAtom.WebUI.Infrastructure.Database.Statics;

namespace CodebaseAtom.WebUI.Infrastructure.Database.Configurations;

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
