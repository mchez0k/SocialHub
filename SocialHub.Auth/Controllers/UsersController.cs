using Microsoft.AspNetCore.Mvc;
using SocialHub.Auth.Application.Commands;
using SocialHub.Auth.Application.Commands.Models;

namespace SocialHub.Auth.Controllers;

[ApiController]
[Route("api/{version}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly RegisterUserCommand registerUserCommand;
    private readonly LoginUserCommand loginUserCommand;

    public UsersController(RegisterUserCommand registerUserCommand,
        LoginUserCommand loginUserCommand)
    {
        this.registerUserCommand = registerUserCommand;
        this.loginUserCommand = loginUserCommand;
    }

    [HttpGet("check")]
    public async Task<IActionResult> Check()
    {
        await Task.CompletedTask;
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserModel model)
    {
        try
        {
            await loginUserCommand.Execute(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Успешный вход");
    }
}