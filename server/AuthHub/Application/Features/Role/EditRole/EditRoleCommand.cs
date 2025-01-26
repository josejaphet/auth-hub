using Application.Abstractions.Messaging;

namespace Application.Features.Role.EditRole;
public sealed record EditRoleCommand(Guid Id, string Name) : ICommand<EditRoleResponse>;
