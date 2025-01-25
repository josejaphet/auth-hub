using Application.Abstractions.Messaging;

namespace Application.Features.Auth.Login;
public sealed record LoginCommand(string Username, string Password) : ICommand<LoginResponse>;