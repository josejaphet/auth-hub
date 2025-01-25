using Microsoft.AspNetCore.Identity;
using SecurityDriven;

namespace Domain.Entities;
public sealed class Role : IdentityRole<Guid>
{
    public static Role Create(string roleName)
    {
        return new Role
        {
            Id = FastGuid.NewGuid(),
            Name = roleName,
            NormalizedName = roleName.ToUpper(),
            ConcurrencyStamp = FastGuid.NewGuid().ToString()
        };
    }
}
