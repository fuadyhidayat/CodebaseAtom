namespace Vioren.CodebaseAtom.WebUI.Logics.Documents.GetDocuments;

public sealed class GetDocumentsLogic(IDbContextFactory<DatabaseContext> databaseContextFactory)
{
    public async Task<GetDocumentsOutput> Handle(GetDocumentsInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var documents = await databaseContext.Documents
            .AsNoTracking()
            .Where(document => document.ProjectId == input.ProjectId)
            .Select(document => new DocumentDto
            {
                Id = document.Id,
                Title = document.Title,
                FileName = document.FileName,
                FileSize = document.FileSize,
                CreatedAt = document.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return new GetDocumentsOutput { Documents = documents };
    }
}
