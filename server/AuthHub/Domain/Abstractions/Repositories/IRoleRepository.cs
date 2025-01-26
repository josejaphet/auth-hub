using Domain.Entities;

namespace Domain.Abstractions.Repositories;
public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetRolesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
