using Microsoft.AspNetCore.Identity;

namespace Vioren.CodebaseExpress.Services.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public required string DisplayName { get; set; }
}
