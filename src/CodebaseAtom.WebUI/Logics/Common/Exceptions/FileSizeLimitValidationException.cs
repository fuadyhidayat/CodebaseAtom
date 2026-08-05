namespace CodebaseAtom.WebUI.Logics.Common.Exceptions;

public sealed class FileSizeLimitValidationException : Exception
{
    public FileSizeLimitValidationException() : base() { }

    public FileSizeLimitValidationException(string message) : base(message) { }

    public FileSizeLimitValidationException(string message, Exception innerException) : base(message, innerException) { }

    public FileSizeLimitValidationException(string fileName, long fileSize, long limitSize)
        : base($"File '{fileName}' with size {fileSize.ToReadableFileSize()} exceeds the limit of {limitSize.ToReadableFileSize()}.")
    {
    }
}
