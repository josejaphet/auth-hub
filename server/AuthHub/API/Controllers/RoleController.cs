using Application.Features.Role.AddRole;
using Application.Features.Role.EditRole;
using Application.Features.Role.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;
    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddRole([FromBody] AddRoleRequest request, CancellationToken cancellationToken)
    {
        var command = new AddRoleCommand(request.Name);

        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("{Id:Guid}")]
    public async Task<IActionResult> EditRole(Guid Id, [FromBody] EditRoleRequest request, CancellationToken cancellationToken)
    {
        var command = new EditRoleCommand(Id, request.Name);

        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles([FromBody] GetRolesRequest request, CancellationToken cancellationToken)
    {
        var query = new GetRolesQuery(request.Pagination);

        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
