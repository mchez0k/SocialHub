namespace SocialHub.Auth.Application.Commands.Models;

public class RegisterUserCommandModel
{
    public string? Email { get; set; }
    
    public string? Password { get; set; }
    
    public string? Name { get; set; }
}