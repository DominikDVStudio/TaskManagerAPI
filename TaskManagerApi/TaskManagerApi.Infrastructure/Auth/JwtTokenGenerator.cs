using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Infrastructure.Auth;

public class JwtTokenGenerator: IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        string? secretKey = _configuration["Jwt:SecretKey"];

        string? issuer = _configuration["Jwt:Issuer"];

        string? audience = _configuration["Jwt:Audience"];

        int expiryMinutes =
            int.Parse(_configuration["Jwt:ExpiryInMinutes"]!);
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),

            new(ClaimTypes.Email, user.Email),
        };
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}