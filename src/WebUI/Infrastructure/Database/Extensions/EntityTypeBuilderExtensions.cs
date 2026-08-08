using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vioren.CodebaseAtom.WebUI.Infrastructure.Database.Statics;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Database.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static void ConfigureFileProperties<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : FileEntity
    {
        _ = builder.Property(x => x.FileName)
            .HasColumnType(ColumnTypeFor.Nvarchar(MaximumLengthFor.FileName));

        _ = builder.Property(x => x.FileContentType)
            .HasColumnType(ColumnTypeFor.Nvarchar(MaximumLengthFor.FileContentType));
    }
}
