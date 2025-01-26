using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Configurations.Security;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AuthHubDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AuthHubDbContext>());

        AddAspIdentity(services, configuration);
        AddPersistence(services);
        return services;
    }

    private static void AddAspIdentity(IServiceCollection services, IConfiguration configuration)
    {
        var identityOption = configuration.GetSection("IdentityOption").Get<IdentityOption>();

        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = identityOption!.PasswordOption.RequireDigit;
            options.Password.RequireLowercase = identityOption!.PasswordOption.RequireLowercase;
            options.Password.RequireUppercase = identityOption!.PasswordOption.RequireUppercase;
            options.Password.RequireNonAlphanumeric = identityOption!.PasswordOption.RequireNonAlphanumeric;
            options.Password.RequiredLength = identityOption.PasswordOption.RequiredLength;

            options.Lockout.AllowedForNewUsers = identityOption.LockoutOption.AllowedForNewUsers;
            options.Lockout.MaxFailedAccessAttempts = identityOption.LockoutOption.MaxFailedAccessAttempts;

            options.User.RequireUniqueEmail = identityOption.UserOption.RequireUniqueEmail;
            options.User.AllowedUserNameCharacters = identityOption.UserOption.AllowedUserNameCharacters;

            options.Stores.ProtectPersonalData = identityOption.StoresOption.ProtectPersonalData;
            options.Stores.MaxLengthForKeys = identityOption.StoresOption.MaxLengthForKeys;

            options.SignIn.RequireConfirmedEmail = identityOption.SignInOption.RequireConfirmedEmail;
            options.SignIn.RequireConfirmedPhoneNumber = identityOption.SignInOption.RequireConfirmedPhoneNumber;

            options.Tokens.AuthenticatorIssuer = identityOption.TokensOption.AuthenticatorIssuer;
            options.Tokens.AuthenticatorTokenProvider = identityOption.TokensOption.AuthenticatorTokenProvider;
            options.Tokens.ChangeEmailTokenProvider = identityOption.TokensOption.ChangeEmailTokenProvider;
            options.Tokens.ChangePhoneNumberTokenProvider = identityOption.TokensOption.ChangePhoneNumberTokenProvider;
        })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AuthHubDbContext>()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<Role>>();

    }

    private static void AddPersistence(IServiceCollection services)
    {
        services.AddScoped<IRoleRepository, RoleRepository>();
    }
}
