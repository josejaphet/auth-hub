using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;
public sealed class UserLogin : IdentityUserLogin<Guid>
{
    public User User { get; set; } = default!;
}
