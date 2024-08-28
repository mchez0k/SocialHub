using Microsoft.AspNetCore.Mvc;

namespace SocialHub.Auth.Controllers;

[ApiController]
[Route("api/{version}/[controller]")]
public class UsersController : ControllerBase
{
    // private readonly RegisterUserCommand registerUserCommand;
    //
    // public UsersController(RegisterUserCommand registerUserCommand)
    // {
    //     this.registerUserCommand = registerUserCommand;
    // }
    
    public UsersController()
    {
    }
    
    [HttpGet("check")]
    public async Task<IActionResult> Check()
    {
        return Ok("Check");
    }

    // [HttpPost("register")]
    // public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
    // {
    //     var result = await _userService.RegisterUserAsync(userRegisterDto);
    //     if (result.Success)
    //     {
    //         return Ok(result);
    //     }
    //
    //     return BadRequest(result.Errors);
    // }
}