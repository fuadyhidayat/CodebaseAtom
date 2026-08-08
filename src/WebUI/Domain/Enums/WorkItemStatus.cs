namespace Vioren.CodebaseAtom.WebUI.Domain.Enums;

public enum WorkItemStatus
{
    NotStarted = 0,
    InProgress = 1,
    Done = 2
}

public static class WorkItemStatusExtensions
{
    extension(WorkItemStatus status)
    {
        public string Name => status switch
        {
            WorkItemStatus.NotStarted => "Not Started",
            WorkItemStatus.InProgress => "In Progress",
            WorkItemStatus.Done => "Done",
            _ => status.ToString()
        };
    }
}
