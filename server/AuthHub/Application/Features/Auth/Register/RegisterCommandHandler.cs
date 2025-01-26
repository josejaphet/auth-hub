using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Application.Features.Auth.Register;
internal class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
{
    private readonly UserManager<User> _userManager;
    public RegisterCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<Guid>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = User.Create(command.UserName,
                                command.Email,
                                command.PhoneNumber);

        if (user == null)
        {
            return Result.Failure<Guid>(new Error("Auth.Register", "User not created"));
        }

        var email = await _userManager.FindByEmailAsync(command.Email);

        if (email != null)
        {
            return Result.Failure<Guid>(new Error("Auth.Register", "Email already exists"));
        }

        var result = await _userManager.CreateAsync(user,
                                                    command.Password);

        if (!result.Succeeded)
        {
            string errors = JsonSerializer.Serialize(result.Errors);

            return Result.Failure<Guid>(new Error("Auth.Register", errors));
        }

        var role = await _userManager.AddToRolesAsync(user,
                                                     command.Roles.Select(x => x.ToString()));

        if (!role.Succeeded)
        {
            string errors = JsonSerializer.Serialize(role.Errors);
            return Result.Failure<Guid>(new Error("Auth.Register", errors));
        }

        return Result<Guid>.Success(user.Id, $"User {command.UserName} added successfully.");
    }
}
