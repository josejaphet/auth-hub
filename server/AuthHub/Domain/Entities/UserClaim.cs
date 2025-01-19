using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;
public sealed class UserClaim : IdentityUserClaim<Guid>
{
    public User User { get; set; } = default!;
}
