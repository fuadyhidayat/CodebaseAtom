using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CodebaseAtom.WebUI.Infrastructure.Database.Extensions;
using CodebaseAtom.WebUI.Infrastructure.Database.Statics;

namespace CodebaseAtom.WebUI.Infrastructure.Database.Configurations;

public sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        _ = builder.ToTable(nameof(DatabaseContext.Documents));

        builder.ConfigureFileProperties();

        _ = builder.Property(entity => entity.Title)
            .HasColumnType(ColumnTypeFor.Nvarchar(MaximumLengthFor.Title));

        _ = builder.HasOne(entity => entity.Project)
            .WithMany(entity => entity.Documents)
            .HasForeignKey(entity => entity.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
