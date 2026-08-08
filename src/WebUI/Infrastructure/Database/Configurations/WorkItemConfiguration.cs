using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Database.Statics;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Database.Configurations;

public sealed class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
{
    public void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        _ = builder.ToTable(nameof(DatabaseContext.WorkItems));

        _ = builder.Property(entity => entity.Title)
            .HasColumnType(ColumnTypeFor.Nvarchar(MaximumLengthFor.Title));

        _ = builder.Property(entity => entity.Description)
            .HasColumnType(ColumnTypeFor.Nvarchar(MaximumLengthFor.Description));

        _ = builder.HasOne(entity => entity.Project)
            .WithMany(entity => entity.WorkItems)
            .HasForeignKey(entity => entity.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
