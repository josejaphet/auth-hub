using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Application.Features.Role.AddRole;
public sealed class AddRoleCommandHandler : ICommandHandler<AddRoleCommand, AddRoleResponse>
{
    private readonly RoleManager<Domain.Entities.Role> roleManager;
    public AddRoleCommandHandler(RoleManager<Domain.Entities.Role> roleManager)
    {
        this.roleManager = roleManager;
    }

    public async Task<Result<AddRoleResponse>> Handle(AddRoleCommand command, CancellationToken cancellationToken)
    {
        if (command is null)
        {
            return Result.Failure<AddRoleResponse>(Error.NullValue);
        }

        var role = Domain.Entities.Role.Create(command.Name);

        if (role is null)
        {
            return Result.Failure<AddRoleResponse>(new Error("Role.AddRole", "Role not created"));
        }

        var result = await roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            string errors = JsonSerializer.Serialize(result.Errors);

            return Result.Failure<AddRoleResponse>(new Error("Role.AddRole", errors));
        }

        return Result.Success(new AddRoleResponse
        {
            RoleName = role.Name!
        });
    }
}
