using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vioren.CodebaseExpress.Domain.Common.Statics;
using Vioren.CodebaseExpress.Infrastructure.Database.Extensions;
using Vioren.CodebaseExpress.Infrastructure.Database.Statics;

namespace Vioren.CodebaseExpress.Infrastructure.Database.Configurations;

public sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        _ = builder.ToTable(nameof(DatabaseService.Documents));

        builder.ConfigureFileProperties();

        _ = builder.Property(entity => entity.Title)
            .HasColumnType(ColumnTypeFor.Nvarchar(MaximumLengthFor.Title));

        _ = builder.HasOne(entity => entity.Project)
            .WithMany(entity => entity.Documents)
            .HasForeignKey(entity => entity.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
