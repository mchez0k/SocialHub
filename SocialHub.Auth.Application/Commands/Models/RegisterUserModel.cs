namespace SocialHub.Auth.Application.Commands.Models;

public class RegisterUserModel
{
    public string? Email { get; set; }
    
    public string? Password { get; set; }
    
    public string? Name { get; set; }
}