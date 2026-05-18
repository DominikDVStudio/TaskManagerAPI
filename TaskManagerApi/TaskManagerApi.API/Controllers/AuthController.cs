using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Application.Commands.Users;
using TaskManagerApi.Application.UseCases.Users.RegisterUser;
using TaskManagerApi.DTOs.Users;
using TaskManagerApi.DTOs.Users.Mappers;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserUseCase _registerUserUseCase;
    private readonly LoginUserUseCase _loginUserUseCase;

    public AuthController(RegisterUserUseCase registerUserUseCase, LoginUserUseCase loginUserUseCase)
    {
        _registerUserUseCase = registerUserUseCase;
        _loginUserUseCase = loginUserUseCase;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        var command = new RegisterUserCommand
        {
            Email = dto.Email,
            Password = dto.Password,
        };

        var result = await _registerUserUseCase.Execute(command);

        return StatusCode(201, UserMapper.UserToDto(result));
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginUserDto dto)
    {
        var command = new LoginUserCommand
        {
            Email = dto.Email,
            Password = dto.Password
        };

        var result = await _loginUserUseCase.Execute(command);

        return Ok(UserMapper.LoginResultToDto(result));
    }
}