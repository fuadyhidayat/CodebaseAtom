namespace CodebaseAtom.WebUI.Domain.Statics;

public static class DomainDisplayTextFor
{
    public const string Title = "Title";
    public const string Description = "Description";
    public const string Status = "Status";

    public const string Id = "ID";
    public const string CreatedAt = "Created At";
    public const string CreatedBy = "Created By";
    public const string User = nameof(User);
    public const string UserId = $"{User} {Id}";
    public const string Username = nameof(Username);
    public const string Password = nameof(Password);
    public const string Code = nameof(Code);
    public const string Name = nameof(Name);
    public const string DisplayName = $"Display {Name}";
    public const string Email = nameof(Email);
    public const string Address = nameof(Address);
    public const string EmailAddress = $"{Email} {Address}";
    public const string Phone = nameof(Phone);
    public const string Number = nameof(Number);
    public const string PhoneNumber = $"{Phone} {Number}";

    public const string File = nameof(File);
    public const string Size = nameof(Size);
    public const string Path = nameof(Path);
    public const string FilePath = $"{File} {Path}";
    public const string FileSize = $"{File} {Size}";
    public const string FileName = $"{File} {Name}";
    public const string Content = nameof(Content);
    public const string Type = nameof(Type);
    public const string ContentType = $"{Content} {Type}";

    public const string Projects = "Projects";
    public const string Project = "Project";
    public const string ProjectDetails = "Project Details";

    public const string WorkItem = "Task"; // Contoh bahwa istilah "WorkItem" disebut sebagai "Task" untuk tampilan pengguna.
    public const string WorkItems = "Tasks"; // Contoh bahwa istilah "WorkItems" disebut sebagai "Tasks" untuk tampilan pengguna.
    public const string Deadline = "Deadline";

    public const string Documents = "Documents";
    public const string Document = "Document";
}
