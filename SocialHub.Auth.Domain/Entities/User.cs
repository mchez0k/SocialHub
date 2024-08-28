namespace SocialHub.Auth.Domain.Entities;

public class User
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Почта пользователя 
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Хэш пароля
    /// </summary>
    public string PasswordHash { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
}