using Application.Abstractions.Messaging;

namespace Application.Features.Auth.Register;
public sealed record RegisterCommand(string UserName,
    string Email,
    string Password,
    string PhoneNumber,
    string[] Roles
    ) : ICommand<Guid>;
