using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;
public sealed class UserRole : IdentityUserRole<Guid>
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public User User { get; set; } = default!;
    public Role Role { get; set; } = default!;
}
