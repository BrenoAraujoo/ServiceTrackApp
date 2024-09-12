using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceTrackHub.Application.Interfaces.Auth;
using ServiceTrackHub.Application.ViewModel.Auth;
using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Application.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration; 
    }
    
    public Token GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["jwt:Key"]);
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new (ClaimTypes.Role, user.Role.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims, "Bearer"),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Token
        {
            AccessToken = tokenHandler.WriteToken(token),
            Expiration = tokenDescriptor.Expires,
        };



    }
}