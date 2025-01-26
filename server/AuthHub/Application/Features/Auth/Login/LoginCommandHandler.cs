using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Login;
public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly UserManager<User> _userManager;
    public LoginCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return Result.Failure<LoginResponse>(Error.NullValue);
        }

        var user = await _userManager.FindByNameAsync(request.Username);

        if (user == null)
        {
            return Result.Failure<LoginResponse>(new Error("Auth.Login", 
                                                           "User not found"));
        }

        var password = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!password)
        {
            return Result.Failure<LoginResponse>(new Error("Auth.Login", 
                                                           "Invalid password"));
        }

        var roles = await _userManager.GetRolesAsync(user);

        if (!roles.Any())
        {
            return Result.Failure<LoginResponse>(new Error("Auth.Login", 
                                                           "Unauthorized"));
        }

        var token = await _userManager.GenerateUserTokenAsync(user, 
                                                                TokenOptions.DefaultProvider, 
                                                                "Token");

        if(string.IsNullOrEmpty(token))
        {
            return Result.Failure<LoginResponse>(new Error("Auth.Login", 
                                                           "Token not generated"));
        }

        var refreshToken = await _userManager.GenerateUserTokenAsync(user, 
                                                                    TokenOptions.DefaultProvider, 
                                                                    "RefreshToken");

        return Result.Success(new LoginResponse
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FullName = string.Empty,
            Token = token,
            RefreshToken = refreshToken
        },"Login successful.");
    }
}
