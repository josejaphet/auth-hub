using Microsoft.AspNetCore.Identity;
using SecurityDriven;

namespace Domain.Entities;
public sealed class User : IdentityUser<Guid>
{

    public static User Create(string userName, string email, string phoneNumber)
    {
        return new User
        {
            Id = FastGuid.NewGuid(),
            UserName = userName,
            NormalizedUserName = userName.ToUpper(),
            Email = email,
            NormalizedEmail = email.ToUpper(),
            PhoneNumber = phoneNumber,
            AccessFailedCount = 0,
            EmailConfirmed = false,
            LockoutEnabled = true,
            TwoFactorEnabled = false,
            PhoneNumberConfirmed = false,
            SecurityStamp = FastGuid.NewGuid().ToString()
            
        };
    }
}
