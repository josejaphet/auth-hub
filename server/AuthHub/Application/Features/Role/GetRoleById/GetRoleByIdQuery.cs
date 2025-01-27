using Application.Abstractions.Messaging;

namespace Application.Features.Role.GetRoleById;
public sealed record GetRoleByIdQuery(Guid Id) : IQuery<GetRoleByIdResponse>;