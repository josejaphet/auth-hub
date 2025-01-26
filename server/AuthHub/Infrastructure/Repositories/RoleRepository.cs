using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public sealed class RoleRepository : IRoleRepository
{
    private readonly AuthHubDbContext _authHubDbContext;
    public RoleRepository(AuthHubDbContext authHubDbContext)
    {
        _authHubDbContext = authHubDbContext;
    }
    public async Task<IEnumerable<Role>> GetRolesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var result = await _authHubDbContext.Roles
                                            .Skip((pageNumber - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync(cancellationToken);

        return result;
    }
}
