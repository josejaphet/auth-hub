using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Application.Features.Role.EditRole;
public sealed class EditRoleCommandHandler : ICommandHandler<EditRoleCommand, EditRoleResponse>
{
    private readonly RoleManager<Domain.Entities.Role> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    public EditRoleCommandHandler(RoleManager<Domain.Entities.Role> roleManager, IUnitOfWork unitOfWork)
    {
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<EditRoleResponse>> Handle(EditRoleCommand command, CancellationToken cancellationToken)
    {
        if (command is null)
        {
            return Result.Failure<EditRoleResponse>(Error.NullValue);
        }

        var role = await _roleManager.FindByIdAsync(command.Id.ToString());

        if (role is null)
        {
            return Result.Failure<EditRoleResponse>(new Error("Role.EditRole", "Role not found"));
        }

        var oldValue = role.Name;

        var result = await _roleManager.SetRoleNameAsync(role, command.Name);

        if (!result.Succeeded)
        {
            string errors = JsonSerializer.Serialize(result.Errors);
            return Result.Failure<EditRoleResponse>(new Error("Role.EditRole", errors));
        }

        if (await _unitOfWork.SaveChangesAsync(cancellationToken) < 1)
        {
            return Result.Failure<EditRoleResponse>(new Error("Role.EditRole", "Role not updated"));
        }

        return Result.Success(new EditRoleResponse
        {
            NewValue = command.Name,
            OldValue = oldValue!
        }, "Role updated successfully.");
    }
}
