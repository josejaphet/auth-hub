using Application.Abstractions.Messaging;

namespace Application.Features.Role.AddRole;
public sealed record AddRoleCommand(string Name): ICommand<AddRoleResponse>;