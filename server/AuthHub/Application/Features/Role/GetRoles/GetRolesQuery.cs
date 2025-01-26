using Application.Abstractions.Messaging;
using Domain;

namespace Application.Features.Role.GetRoles;
public sealed record GetRolesQuery(Pagination Pagination) : IQuery<IEnumerable<GetRolesResponse>>;