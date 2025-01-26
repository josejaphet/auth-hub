using Application.Exceptions;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class AuthHubDbContext(DbContextOptions options)
    : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options), IUnitOfWork
{

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency Exception occurred.", ex);
        }
    }
}
