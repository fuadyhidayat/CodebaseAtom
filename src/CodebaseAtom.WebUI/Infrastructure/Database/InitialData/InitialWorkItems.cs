using CodebaseAtom.WebUI.Infrastructure.Identity.Database.InitialData;

namespace CodebaseAtom.WebUI.Infrastructure.Database.InitialData;

public static class InitialWorkItems
{
    public static readonly WorkItem HrisRequirementsGathering = new()
    {
        Id = new Guid("019ecf56-31df-770a-9bcf-a25439af4919"),
        ProjectId = InitialProjects.Hris.Id,
        Title = "Requirements Gathering",
        Description = "Gathering requirements for the HRIS project",
        Deadline = new DateOnly(2026, 7, 31),
        Status = WorkItemStatus.InProgress,
        CreatedBy = InitialUsers.Administrator.Id
    };

    public static readonly WorkItem HrisAnalysisAndDesign = new()
    {
        Id = new Guid("019ecf56-31df-718d-a39f-756abe0f5514"),
        ProjectId = InitialProjects.Hris.Id,
        Title = "Analysis and Design",
        Description = "Analyzing and designing the HRIS project",
        Deadline = new DateOnly(2026, 8, 31),
        Status = WorkItemStatus.NotStarted,
        CreatedBy = InitialUsers.Administrator.Id
    };

    public static readonly WorkItem DataWarehousePrepareInfrastructure = new()
    {
        Id = new Guid("019ecf56-31df-7815-86bc-acd183ff7926"),
        ProjectId = InitialProjects.DataWarehouse.Id,
        Title = "Prepare Infrastructure",
        Description = "Preparing infrastructure for the Data Warehouse project",
        Deadline = new DateOnly(2026, 7, 23),
        Status = WorkItemStatus.Done,
        CreatedBy = InitialUsers.Administrator.Id
    };

    public static readonly WorkItem DataWarehouseDevelopReport = new()
    {
        Id = new Guid("019ecf56-31df-7f50-a56a-592713235ebd"),
        ProjectId = InitialProjects.DataWarehouse.Id,
        Title = "Develop Report",
        Description = "Developing reports for the Data Warehouse project",
        Deadline = new DateOnly(2026, 8, 9),
        Status = WorkItemStatus.InProgress,
        CreatedBy = InitialUsers.Administrator.Id
    };

    public static IReadOnlyCollection<WorkItem> All =>
    [
        HrisRequirementsGathering,
        HrisAnalysisAndDesign,
        DataWarehousePrepareInfrastructure,
        DataWarehouseDevelopReport
    ];
}
