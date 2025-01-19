using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;
public sealed class UserRole : IdentityUserRole<Guid>
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
}
