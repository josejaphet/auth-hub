namespace Application.Features.Auth.Login;
public class LoginResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string[] Roles { get; set; } = default!;
    public string Token { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
