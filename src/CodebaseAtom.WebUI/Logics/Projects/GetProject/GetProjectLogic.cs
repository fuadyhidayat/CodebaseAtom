using CodebaseAtom.WebUI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace CodebaseAtom.WebUI.Logics.Projects.GetProject;

public sealed class GetProjectLogic(DatabaseContext databaseContext, UserManager<ApplicationUser> userManager)
{
    public async Task<GetProjectOutput> Handle(GetProjectInput input, CancellationToken cancellationToken = default)
    {
        var item = await databaseContext.Projects
            .AsNoTracking()
            .Where(project => project.Id == input.Id)
            .Select(project => new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                CreatedAt = project.Created,
                CreatedBy = project.CreatedBy
            })
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.Project, DomainDisplayTextFor.Id, input.Id);

        var user = await userManager.FindByIdAsync(item.CreatedBy.ToString());

        if (user is not null)
        {
            item.CreatedByUsername = user.UserName ?? string.Empty;
            item.CreatedByEmail = user.Email ?? string.Empty;
            item.CreatedByDisplayName = user.DisplayName;
        }

        return new GetProjectOutput { Item = item };
    }
}
