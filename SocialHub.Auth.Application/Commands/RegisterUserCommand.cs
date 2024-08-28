using SocialHub.Auth.Application.Commands.Models;
using SocialHub.Auth.Persistance;

namespace SocialHub.Auth.Application.Commands;

public class RegisterUserCommand
{
    #region Fields
    
    //private readonly ILogger<> _logger;
    private readonly UsersRepository usersRepository;

    #endregion
    
    #region Constructors
    
    public RegisterUserCommand(UsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }
    
    #endregion
    
    public void Execute(RegisterUserCommandModel model)
    {
        //await usersRepository.CreateAsync(model);
    }
}