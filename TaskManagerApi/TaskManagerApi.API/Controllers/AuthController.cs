using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Application.Commands.Users;
using TaskManagerApi.Application.UseCases.Users.RegisterUser;
using TaskManagerApi.DTOs.Users;
using TaskManagerApi.DTOs.Users.Mappers;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    private readonly RegisterUserUseCase _registerUserUseCase;

    public AuthController(RegisterUserUseCase registerUserUseCase)
    {
        _registerUserUseCase = registerUserUseCase;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        var command = new RegisterUserCommand
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = dto.Password,
        };

        var result = await _registerUserUseCase.Execute(command);
        
        return StatusCode(201, UserMapper.ToDto(result));
    }
}