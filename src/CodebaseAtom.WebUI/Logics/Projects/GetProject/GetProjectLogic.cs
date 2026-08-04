namespace CodebaseAtom.WebUI.Logics.Projects.GetProject;

public sealed class GetProjectLogic(IDatabaseService databaseService)
{
    public async Task<GetProjectOutput> Handle(GetProjectInput input, CancellationToken cancellationToken = default)
    {
        var item = await databaseService.Projects
            .AsNoTracking()
            .Where(project => project.Id == input.Id)
            .Select(project => new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                CreatedAt = project.Created
            })
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Project, DomainDisplayTextFor.Id, input.Id);

        return new GetProjectOutput { Item = item };
    }
}
