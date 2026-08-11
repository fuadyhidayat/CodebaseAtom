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

        var projects = await databaseContext.Projects
            .AsNoTracking()
            .Where(project => project.Id == input.ProjectId)
            .Select(project => new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                CreatedBy = project.CreatedBy,
                UpdatedAt = project.UpdatedAt,
                UpdatedBy = project.UpdatedBy
            })
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Project, DomainDisplayTextFor.Id, input.ProjectId);

        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var userCreatedBy = await userManager.FindByIdAsync(projects.CreatedBy.ToString());

        if (userCreatedBy is not null)
        {
            projects.CreatedByUsername = userCreatedBy.UserName ?? string.Empty;
            projects.CreatedByEmail = userCreatedBy.Email ?? string.Empty;
            projects.CreatedByDisplayName = userCreatedBy.DisplayName;
        }

        if (projects.UpdatedBy.HasValue)
        {
            if (projects.UpdatedBy.Value != projects.CreatedBy)
            {
                var userUpdatedBy = await userManager.FindByIdAsync(projects.UpdatedBy.Value.ToString());

                if (userUpdatedBy is not null)
                {
                    projects.UpdatedByUsername = userUpdatedBy.UserName ?? string.Empty;
                    projects.UpdatedByEmail = userUpdatedBy.Email ?? string.Empty;
                    projects.UpdatedByDisplayName = userUpdatedBy.DisplayName;
                }
            }
            else
            {
                projects.UpdatedByUsername = projects.CreatedByUsername;
                projects.UpdatedByEmail = projects.CreatedByEmail;
                projects.UpdatedByDisplayName = projects.CreatedByDisplayName;
            }
        }

        return new GetProjectOutput { Project = projects };
    }
}
