using CodebaseAtom.WebUI.Infrastructure.FileStorage;

namespace CodebaseAtom.WebUI.Logics.Documents.CreateDocument;

public sealed class CreateDocumentLogic(IDatabaseService databaseService, IFileStorageService fileStorageService)
{
    public async Task<CreateDocumentOutput> Handle(CreateDocumentInput input, CancellationToken cancellationToken = default)
    {
        var document = new Document
        {
            ProjectId = input.ProjectId,
            Title = input.Title,
            FileName = input.FileName,
            FileContentType = input.ContentType,
            FileSize = input.FileSize
        };

        _ = await databaseService.Documents.AddAsync(document, cancellationToken);

        var fileBytes = input.FileContent.ToArray();

        await fileStorageService.CreateAsync(document.FilePath, fileBytes, cancellationToken);
        _ = await databaseService.SaveChangesAsync(cancellationToken);

        return new CreateDocumentOutput
        {
            DocumentId = document.Id
        };
    }
}
