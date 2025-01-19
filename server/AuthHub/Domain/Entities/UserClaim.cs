using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;
public sealed class UserClaim : IdentityUserClaim<Guid>
{
    public override Guid Id { get; set; }
    public User User { get; set; } = default!;
}
