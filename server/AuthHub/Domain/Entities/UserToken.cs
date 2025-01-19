using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;
public sealed class UserToken : IdentityUserToken<Guid>
{
}
