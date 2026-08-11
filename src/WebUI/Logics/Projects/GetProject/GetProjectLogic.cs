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
                CreatedAt = project.CreatedAt,
                CreatedBy = project.CreatedBy,
                UpdatedAt = project.UpdatedAt,
                UpdatedBy = project.UpdatedBy
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

        if (item.UpdatedBy.HasValue)
        {
            if (item.UpdatedBy.Value != item.CreatedBy)
            {
                var userUpdatedBy = await userManager.FindByIdAsync(item.UpdatedBy.Value.ToString());

                if (userUpdatedBy is not null)
                {
                    item.UpdatedByUsername = userUpdatedBy.UserName ?? string.Empty;
                    item.UpdatedByEmail = userUpdatedBy.Email ?? string.Empty;
                    item.UpdatedByDisplayName = userUpdatedBy.DisplayName;
                }
            }
            else
            {
                item.UpdatedByUsername = item.CreatedByUsername;
                item.UpdatedByEmail = item.CreatedByEmail;
                item.UpdatedByDisplayName = item.CreatedByDisplayName;
            }
        }

        return new GetProjectOutput { Project = item };
    }
}
