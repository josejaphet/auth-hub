using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;
public sealed class User : IdentityUser<Guid>
{
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public ICollection<UserToken> UserTokens { get; set; } = [];
    public ICollection<UserClaim> UserClaims { get; set; } = [];
}
