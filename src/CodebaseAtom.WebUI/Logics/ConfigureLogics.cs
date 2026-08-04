using CodebaseAtom.WebUI.Logics.Common.Behaviors;
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
        _ = services.AddTransient(typeof(ILogicBehavior<,>), typeof(LoggingBehavior<,>));
        _ = services.AddTransient(typeof(ILogicBehavior<,>), typeof(PerformanceBehavior<,>));

        _ = services.AddLogicWithPipeline<GetProjectsLogic, GetProjectsInput, GetProjectsOutput>();
        _ = services.AddLogicWithPipeline<GetProjectLogic, GetProjectInput, GetProjectOutput>();
        _ = services.AddLogicWithPipeline<CreateProjectLogic, CreateProjectInput, CreateProjectOutput>();
        _ = services.AddLogicWithPipeline<UpdateProjectLogic, UpdateProjectInput, Unit>();
        _ = services.AddLogicWithPipeline<DeleteProjectLogic, DeleteProjectInput, Unit>();

        _ = services.AddLogicWithPipeline<GetWorkItemsLogic, GetWorkItemsInput, GetWorkItemsOutput>();
        _ = services.AddLogicWithPipeline<CreateWorkItemLogic, CreateWorkItemInput, CreateWorkItemOutput>();
        _ = services.AddLogicWithPipeline<UpdateWorkItemLogic, UpdateWorkItemInput, Unit>();
        _ = services.AddLogicWithPipeline<UpdateWorkItemStatusLogic, UpdateWorkItemStatusInput, Unit>();
        _ = services.AddLogicWithPipeline<DeleteWorkItemLogic, DeleteWorkItemInput, Unit>();

        _ = services.AddLogicWithPipeline<GetDocumentsLogic, GetDocumentsInput, GetDocumentsOutput>();
        _ = services.AddLogicWithPipeline<CreateDocumentLogic, CreateDocumentInput, CreateDocumentOutput>();
        _ = services.AddLogicWithPipeline<UpdateDocumentLogic, UpdateDocumentInput, Unit>();
        _ = services.AddLogicWithPipeline<DeleteDocumentLogic, DeleteDocumentInput, Unit>();
        _ = services.AddLogicWithPipeline<DownloadDocumentLogic, DownloadDocumentInput, DownloadDocumentOutput>();

        _ = services.AddLogicWithPipeline<GetStatisticLogic, GetStatisticInput, GetStatisticOutput>();

        return services;
    }
}
