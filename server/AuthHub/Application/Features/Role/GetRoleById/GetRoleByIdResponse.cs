namespace Application.Features.Role.GetRoleById;
public class GetRoleByIdResponse
{
    public Guid RoleId { get; set; }
    public string Name { get; set; } = default!;
}
