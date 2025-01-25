namespace Application.Features.Auth.Register;
public sealed record RegisterRequest(
    string UserName,
    string Email,
    string Password,
    string PhoneNumber,
    string[] Roles
    );