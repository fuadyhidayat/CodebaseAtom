using Vioren.CodebaseAtom.WebUI.Logics.Documents.CreateDocument;
using Vioren.CodebaseAtom.WebUI.Logics.Documents.DeleteDocument;
using Vioren.CodebaseAtom.WebUI.Logics.Documents.DownloadDocument;
using Vioren.CodebaseAtom.WebUI.Logics.Documents.GetDocuments;
using Vioren.CodebaseAtom.WebUI.Logics.Documents.UpdateDocument;
using Vioren.CodebaseAtom.WebUI.Logics.Projects.CreateProject;
using Vioren.CodebaseAtom.WebUI.Logics.Projects.DeleteProject;
using Vioren.CodebaseAtom.WebUI.Logics.Projects.GetProject;
using Vioren.CodebaseAtom.WebUI.Logics.Projects.GetProjects;
using Vioren.CodebaseAtom.WebUI.Logics.Projects.UpdateProject;
using Vioren.CodebaseAtom.WebUI.Logics.Statistics.GetStatistic;
using Vioren.CodebaseAtom.WebUI.Logics.Users.GetCurrentUser;
using Vioren.CodebaseAtom.WebUI.Logics.Users.UpdatePassword;
using Vioren.CodebaseAtom.WebUI.Logics.Users.UpdateUser;
using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.CreateWorkItem;
using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItem;
using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.DeleteWorkItems;
using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.GetWorkItems;
using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;
using Vioren.CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItemStatus;

namespace Vioren.CodebaseAtom.WebUI.Logics;

public static class ConfigureLogics
{
    public static WebApplicationBuilder AddLogics(this WebApplicationBuilder builder)
    {
        _ = builder.Services.AddScoped<GetProjectsLogic>();
        _ = builder.Services.AddScoped<GetProjectLogic>();
        _ = builder.Services.AddScoped<CreateProjectLogic>();
        _ = builder.Services.AddScoped<UpdateProjectLogic>();
        _ = builder.Services.AddScoped<DeleteProjectLogic>();

        _ = builder.Services.AddScoped<GetWorkItemsLogic>();
        _ = builder.Services.AddScoped<CreateWorkItemLogic>();
        _ = builder.Services.AddScoped<UpdateWorkItemLogic>();
        _ = builder.Services.AddScoped<UpdateWorkItemStatusLogic>();
        _ = builder.Services.AddScoped<DeleteWorkItemLogic>();
        _ = builder.Services.AddScoped<DeleteWorkItemsLogic>();

        _ = builder.Services.AddScoped<GetDocumentsLogic>();
        _ = builder.Services.AddScoped<DownloadDocumentLogic>();
        _ = builder.Services.AddScoped<CreateDocumentLogic>();
        _ = builder.Services.AddScoped<UpdateDocumentLogic>();
        _ = builder.Services.AddScoped<DeleteDocumentLogic>();

        _ = builder.Services.AddScoped<GetCurrentUserLogic>();
        _ = builder.Services.AddScoped<UpdateUserLogic>();
        _ = builder.Services.AddScoped<UpdatePasswordLogic>();

        _ = builder.Services.AddScoped<GetStatisticLogic>();

        return builder;
    }
}
