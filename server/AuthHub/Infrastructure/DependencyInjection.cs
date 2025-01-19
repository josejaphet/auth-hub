using Domain.Entities;
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

        services.AddAspIdentity(configuration);
        return services;
    }

    public static IServiceCollection AddAspIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityCore<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AuthHubDbContext>()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<Role>>();

        return services;
    }
}
