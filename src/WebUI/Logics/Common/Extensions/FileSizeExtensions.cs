namespace Vioren.CodebaseAtom.WebUI.Logics.Common.Extensions;

public static class FileSizeExtensions
{
    public static string ToReadableFileSize(this long fileSize)
    {
        var sizes = new string[] { "B", "KB", "MB", "GB", "TB" };
        var length = Convert.ToDouble(fileSize);
        var order = 0;

        while (length >= 1024 && order < sizes.Length - 1)
        {
            order++;
            length /= 1024;
        }

        return string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1}", length, sizes[order]);
    }
}
