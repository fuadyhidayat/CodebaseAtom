using Microsoft.Extensions.DependencyInjection;
using Vioren.CodebaseExpress.Logics.Common.Behaviors;
using Vioren.CodebaseExpress.Logics.Documents.CreateDocument;
using Vioren.CodebaseExpress.Logics.Documents.DeleteDocument;
using Vioren.CodebaseExpress.Logics.Documents.DownloadDocument;
using Vioren.CodebaseExpress.Logics.Documents.GetDocuments;
using Vioren.CodebaseExpress.Logics.Documents.UpdateDocument;
using Vioren.CodebaseExpress.Logics.Projects.CreateProject;
using Vioren.CodebaseExpress.Logics.Projects.DeleteProject;
using Vioren.CodebaseExpress.Logics.Projects.GetProject;
using Vioren.CodebaseExpress.Logics.Projects.GetProjects;
using Vioren.CodebaseExpress.Logics.Projects.UpdateProject;
using Vioren.CodebaseExpress.Logics.Statistics.GetStatistic;
using Vioren.CodebaseExpress.Logics.WorkItems.CreateWorkItem;
using Vioren.CodebaseExpress.Logics.WorkItems.DeleteWorkItem;
using Vioren.CodebaseExpress.Logics.WorkItems.GetWorkItems;
using Vioren.CodebaseExpress.Logics.WorkItems.UpdateWorkItem;
using Vioren.CodebaseExpress.Logics.WorkItems.UpdateWorkItemStatus;

namespace Vioren.CodebaseExpress.Logics;

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
