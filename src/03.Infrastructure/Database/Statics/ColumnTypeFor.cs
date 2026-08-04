namespace Vioren.CodebaseExpress.Infrastructure.Database.Statics;

public static class ColumnTypeFor
{
    public const string Money = "money";

    public static string Nvarchar(int length)
    {
        return $"nvarchar({length})";
    }
}
