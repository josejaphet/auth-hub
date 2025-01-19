using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;
public sealed class UserToken : IdentityUserToken<Guid>
{
    public User User { get; set; } = default!;
}
