namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.GetDocuments;

public sealed class GetDocumentsLogic(DatabaseContext databaseContext)
{
    public async Task<GetDocumentsOutput> Handle(GetDocumentsInput input, CancellationToken cancellationToken = default)
    {
        var items = await databaseContext.Documents
            .AsNoTracking()
            .Where(document => document.ProjectId == input.ProjectId)
            .Select(document => new DocumentDto
            {
                Id = document.Id,
                Title = document.Title,
                FileName = document.FileName,
                FileSize = document.FileSize,
                CreatedAt = document.Created
            })
            .ToListAsync(cancellationToken);

        return new GetDocumentsOutput { Items = items };
    }
}
