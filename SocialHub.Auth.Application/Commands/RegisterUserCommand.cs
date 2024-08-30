using SocialHub.Auth.Application.Commands.Models;
using SocialHub.Auth.Domain.Entities;
using SocialHub.Auth.Persistance;
using SocialHub.Shared;

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
    
    public async Task Execute(RegisterUserCommandModel model)
    {
        if (model.Email is null || model.Password is null || model.Name is null)
        {
            throw new ArgumentNullException(nameof(model.Email));
        }
        await usersRepository.CreateAsync(new User
        {
            Email = model.Email,
            PasswordHash = model.Password,
            Name = model.Name
        });
    }
}