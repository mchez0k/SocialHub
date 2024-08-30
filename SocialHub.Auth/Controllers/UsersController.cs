using Microsoft.AspNetCore.Mvc;
using SocialHub.Auth.Application.Commands;
using SocialHub.Auth.Application.Commands.Models;

namespace SocialHub.Auth.Controllers;

[ApiController]
[Route("api/{version}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly RegisterUserCommand registerUserCommand;
    
    public UsersController(RegisterUserCommand registerUserCommand)
    {
        this.registerUserCommand = registerUserCommand;
    }
    
    [HttpGet("check")]
    public async Task<IActionResult> Check()
    {
        await Task.CompletedTask;
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommandModel model)
    {
        try
        {
            await registerUserCommand.Execute(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Зарегистрирован новый пользователь!");
    }
}