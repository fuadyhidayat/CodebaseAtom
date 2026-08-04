using CodebaseAtom.WebUI.Logics.Documents.CreateDocument;
using CodebaseAtom.WebUI.Logics.Documents.DeleteDocument;
using CodebaseAtom.WebUI.Logics.Documents.DownloadDocument;
using CodebaseAtom.WebUI.Logics.Documents.GetDocuments;
using CodebaseAtom.WebUI.Logics.Documents.UpdateDocument;
using CodebaseAtom.WebUI.Logics.Projects.CreateProject;
using CodebaseAtom.WebUI.Logics.Projects.DeleteProject;
using CodebaseAtom.WebUI.Logics.Projects.GetProject;
using CodebaseAtom.WebUI.Logics.Projects.GetProjects;
using CodebaseAtom.WebUI.Logics.Projects.UpdateProject;
using CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;
using CodebaseAtom.WebUI.Logics.WorkItems.CreateWorkItem;
using CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItem;
using CodebaseAtom.WebUI.Logics.WorkItems.GetWorkItems;
using CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;
using CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItemStatus;

namespace CodebaseAtom.WebUI.Logics;

public static class ConfigureLogics
{
    public static IServiceCollection AddLogics(this IServiceCollection services)
    {
        _ = services.AddTransient<GetProjectsLogic>();
        _ = services.AddTransient<GetProjectLogic>();
        _ = services.AddTransient<CreateProjectLogic>();
        _ = services.AddTransient<UpdateProjectLogic>();
        _ = services.AddTransient<DeleteProjectLogic>();

        _ = services.AddTransient<GetWorkItemsLogic>();
        _ = services.AddTransient<CreateWorkItemLogic>();
        _ = services.AddTransient<UpdateWorkItemLogic>();
        _ = services.AddTransient<UpdateWorkItemStatusLogic>();
        _ = services.AddTransient<DeleteWorkItemLogic>();

        _ = services.AddTransient<GetDocumentsLogic>();
        _ = services.AddTransient<DownloadDocumentLogic>();
        _ = services.AddTransient<CreateDocumentLogic>();
        _ = services.AddTransient<UpdateDocumentLogic>();
        _ = services.AddTransient<DeleteDocumentLogic>();

        _ = services.AddTransient<GetStatisticLogic>();

        return services;
    }
}
