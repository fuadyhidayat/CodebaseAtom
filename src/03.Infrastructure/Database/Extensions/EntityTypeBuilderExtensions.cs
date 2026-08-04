using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vioren.CodebaseExpress.Domain.Common.Abstracts;
using Vioren.CodebaseExpress.Domain.Common.Statics;
using Vioren.CodebaseExpress.Infrastructure.Database.Statics;

namespace Vioren.CodebaseExpress.Infrastructure.Database.Extensions;

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
