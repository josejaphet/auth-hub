using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Role.GetRoleById;
public sealed class GetRoleIdQueryHandler : IQueryHandler<GetRoleByIdQuery, GetRoleByIdResponse>
{
    private readonly RoleManager<Domain.Entities.Role> _roleManager;
    public GetRoleIdQueryHandler(RoleManager<Domain.Entities.Role> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task<Result<GetRoleByIdResponse>> Handle(GetRoleByIdQuery query, CancellationToken cancellationToken)
    {
        if (query is null)
        {
            return Result.Failure<GetRoleByIdResponse>(Error.NullValue);
        }

        var result = await _roleManager.FindByIdAsync(query.Id.ToString());

        if (result is null)
        {
            return Result.Success(new GetRoleByIdResponse(), "No data found.");
        }

        return Result.Success(new GetRoleByIdResponse
        {
            RoleId = result.Id,
            Name = result.Name!
        }, "Data found.");
    }
}
