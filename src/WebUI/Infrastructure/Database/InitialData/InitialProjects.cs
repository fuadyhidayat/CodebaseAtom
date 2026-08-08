using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity.Database.InitialData;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Database.InitialData;

public static class InitialProjects
{
    public static readonly Project Hris = new()
    {
        Id = new Guid("019ec037-2998-7e88-bb7d-9d42d157baa4"),
        Title = "Human Resources Information System",
        Description = "A comprehensive HRIS solution for managing employee data, payroll, and benefits.",
        CreatedBy = InitialUsers.Administrator.Id
    };

    public static readonly Project DataWarehouse = new()
    {
        Id = new Guid("019ec037-2998-727b-8d3f-b33c0c81196e"),
        Title = "Data Warehouse",
        Description = "A centralized repository for storing and analyzing large volumes of data from multiple sources.",
        CreatedBy = InitialUsers.Administrator.Id
    };

    public static IReadOnlyCollection<Project> All =>
    [
        Hris,
        DataWarehouse
    ];
}
