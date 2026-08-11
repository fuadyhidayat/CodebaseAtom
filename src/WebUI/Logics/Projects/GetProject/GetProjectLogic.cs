using Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Vioren.CodebaseAtom.WebUI.Logics.Projects.GetProject;

public sealed class GetProjectLogic(
    IDbContextFactory<DatabaseContext> databaseContextFactory,
    IServiceScopeFactory serviceScopeFactory)
{
    public async Task<GetProjectOutput> Handle(GetProjectInput input, CancellationToken cancellationToken = default)
    {
        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var item = await databaseContext.Projects
            .AsNoTracking()
            .Where(project => project.Id == input.ProjectId)
            .Select(project => new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                CreatedAt = project.Created,
                CreatedBy = project.CreatedBy,
                ModifiedAt = project.Modified,
                ModifiedBy = project.ModifiedBy
            })
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Project, DomainDisplayTextFor.Id, input.ProjectId);

        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var userCreatedBy = await userManager.FindByIdAsync(item.CreatedBy.ToString());

        if (userCreatedBy is not null)
        {
            item.CreatedByUsername = userCreatedBy.UserName ?? string.Empty;
            item.CreatedByEmail = userCreatedBy.Email ?? string.Empty;
            item.CreatedByDisplayName = userCreatedBy.DisplayName;
        }

        if (item.ModifiedBy.HasValue)
        {
            if (item.ModifiedBy.Value != item.CreatedBy)
            {
                var userModifiedBy = await userManager.FindByIdAsync(item.ModifiedBy.Value.ToString());

                if (userModifiedBy is not null)
                {
                    item.ModifiedByUsername = userModifiedBy.UserName ?? string.Empty;
                    item.ModifiedByEmail = userModifiedBy.Email ?? string.Empty;
                    item.ModifiedByDisplayName = userModifiedBy.DisplayName;
                }
            }
            else
            {
                item.ModifiedByUsername = item.CreatedByUsername;
                item.ModifiedByEmail = item.CreatedByEmail;
                item.ModifiedByDisplayName = item.CreatedByDisplayName;
            }
        }

        return new GetProjectOutput { Project = item };
    }
}
