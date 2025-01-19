namespace Infrastructure.Configurations.Security;
public sealed class IdentityOption
{
    public PasswordOption PasswordOption { get; set; } = default!;
    public UserOption UserOption { get; set; } = default!;
    public StoresOption StoresOption { get; set; } = default!;
    public SignInOption SignInOption { get; set; } = default!;
    public TokensOption TokensOption { get; set; } = default!;
    public LockoutOption LockoutOption { get; set; } = default!;
}


public class PasswordOption
{
    public bool RequireDigit { get; set; }
    public int RequiredLength { get; set; }
    public bool RequireLowercase { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
    public bool RequireUppercase { get; set; }
}

public class UserOption
{
    public bool RequireUniqueEmail { get; set; }
    public string AllowedUserNameCharacters { get; set; } = default!;
}

public class StoresOption
{
    public bool ProtectPersonalData { get; set; }
    public int MaxLengthForKeys { get; set; }
}

public class SignInOption
{
    public bool RequireConfirmedEmail { get; set; }
    public bool RequireConfirmedPhoneNumber { get; set; }
}

public class TokensOption
{
    public string AuthenticatorIssuer { get; set; } = default!;
    public string AuthenticatorTokenProvider { get; set; } = default!;
    public string ChangeEmailTokenProvider { get; set; } = default!;
    public string ChangePhoneNumberTokenProvider { get; set; } = default!;
}

public class LockoutOption
{
    public bool AllowedForNewUsers { get; set; }
    public int MaxFailedAccessAttempts { get; set; }
}