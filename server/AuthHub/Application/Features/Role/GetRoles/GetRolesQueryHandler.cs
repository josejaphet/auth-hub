using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;

namespace Application.Features.Role.GetRoles;
public sealed class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, IEnumerable<GetRolesResponse>>
{
    private readonly IRoleRepository _roleRepository;
    public GetRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async Task<Result<IEnumerable<GetRolesResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var result = await _roleRepository.GetRolesAsync(
                                        request.Pagination.PageNumber,
                                        request.Pagination.PageSize, 
                                        cancellationToken);


        var mappedResult = Map(result);

        return Result.Success(mappedResult, $"Retrieved {mappedResult.Count()} data.");
    }

    private static IEnumerable<GetRolesResponse> Map(IEnumerable<Domain.Entities.Role> roles)
    {
        return roles.Select(role => new GetRolesResponse(role.Id, role.Name));
    }
}
