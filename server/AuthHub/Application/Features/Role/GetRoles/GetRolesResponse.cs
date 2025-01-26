namespace Application.Features.Role.GetRoles;
public record GetRolesResponse(Guid RoleId, string Name = default!);