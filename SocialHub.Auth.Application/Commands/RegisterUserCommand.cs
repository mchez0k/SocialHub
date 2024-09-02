using Microsoft.EntityFrameworkCore;
using SocialHub.Auth.Application.Commands.Models;
using SocialHub.Auth.Domain.Entities;
using SocialHub.Shared;
using System.Security.Cryptography;

namespace SocialHub.Auth.Application.Commands;

public class RegisterUserCommand
{
    #region Fields
    
    //private readonly ILogger<> _logger;
    private readonly IRepository<User> usersRepository;

    #endregion
    
    #region Constructors
    
    public RegisterUserCommand(IRepository<User> usersRepository)
    {
        this.usersRepository = usersRepository;
    }
    
    #endregion
    
    public async Task Execute(RegisterUserModel model)
    {
        if (model.Email is null || model.Password is null || model.Name is null)
            throw new ArgumentNullException("Ошибка данных пользователя");
        if (!IsValidEmail(model.Email))
            throw new ArgumentNullException("Неверный email");
        var existingUser = usersRepository.Get();
        if (existingUser.Any(u => u.Email == model.Email || u.Name == model.Name))
            throw new InvalidOperationException("Почта или имя уже используются");

        #region Hashing

        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(20);
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
        string savedPasswordHash = Convert.ToBase64String(hashBytes);

        #endregion

        await usersRepository.CreateAsync(new User
        {
            Email = model.Email,
            PasswordHash = savedPasswordHash,
            Name = model.Name
        });
        await usersRepository.SaveChangesAsync();
    }

    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();
        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}