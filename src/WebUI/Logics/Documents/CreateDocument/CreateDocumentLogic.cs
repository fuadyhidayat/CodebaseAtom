using Vioren.CodebaseAtom.WebUI.Infrastructure.FileStorage;

namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.CreateDocument;

public sealed class CreateDocumentLogic(
    IDbContextFactory<DatabaseContext> databaseContextFactory,
    FileStorageService fileStorageService)
{
    public async Task<CreateDocumentOutput> Handle(CreateDocumentInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var document = new Document
        {
            ProjectId = input.ProjectId,
            Title = input.Title,
            FileName = input.FileName,
            FileContentType = input.ContentType,
            FileSize = input.FileSize
        };

        _ = await databaseContext.Documents.AddAsync(document, cancellationToken);

        var fileBytes = input.FileContent.ToArray();

        await fileStorageService.CreateAsync(document.FilePath, fileBytes, cancellationToken);
        _ = await databaseContext.SaveChangesAsync(cancellationToken);

        return new CreateDocumentOutput
        {
            DocumentId = document.Id
        };
    }
}
