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
using CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItems;
using CodebaseAtom.WebUI.Logics.WorkItems.GetWorkItems;
using CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;
using CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItemStatus;

namespace CodebaseAtom.WebUI.Logics;

public static class ConfigureLogics
{
    public static WebApplicationBuilder AddLogics(this WebApplicationBuilder builder)
    {
        _ = builder.Services.AddTransient<GetProjectsLogic>();
        _ = builder.Services.AddTransient<GetProjectLogic>();
        _ = builder.Services.AddTransient<CreateProjectLogic>();
        _ = builder.Services.AddTransient<UpdateProjectLogic>();
        _ = builder.Services.AddTransient<DeleteProjectLogic>();

        _ = builder.Services.AddTransient<GetWorkItemsLogic>();
        _ = builder.Services.AddTransient<CreateWorkItemLogic>();
        _ = builder.Services.AddTransient<UpdateWorkItemLogic>();
        _ = builder.Services.AddTransient<UpdateWorkItemStatusLogic>();
        _ = builder.Services.AddTransient<DeleteWorkItemLogic>();
        _ = builder.Services.AddTransient<DeleteWorkItemsLogic>();

        _ = builder.Services.AddTransient<GetDocumentsLogic>();
        _ = builder.Services.AddTransient<DownloadDocumentLogic>();
        _ = builder.Services.AddTransient<CreateDocumentLogic>();
        _ = builder.Services.AddTransient<UpdateDocumentLogic>();
        _ = builder.Services.AddTransient<DeleteDocumentLogic>();

        _ = builder.Services.AddTransient<GetStatisticLogic>();

        return builder;
    }
}
