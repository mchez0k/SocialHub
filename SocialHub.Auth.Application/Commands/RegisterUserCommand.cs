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
    
    public void Execute(RegisterUserCommandModel model)
    {
        //await usersRepository.CreateAsync(model);
    }
}