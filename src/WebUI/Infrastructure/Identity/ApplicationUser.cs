using Microsoft.AspNetCore.Identity;

namespace Vioren.CodebaseAtom.WebUI.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public required string DisplayName { get; set; }
}
