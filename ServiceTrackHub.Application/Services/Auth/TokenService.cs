using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceTrackHub.Application.Interfaces.Auth;
using ServiceTrackHub.Application.ViewModel.Auth;
using ServiceTrackHub.Domain.Common.Utils;
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

        var key = Encoding.ASCII.GetBytes(_configuration["jwt:Key"]
                        ?? throw new ArgumentNullException());

        var accessTokenExpirationMinutes = double.Parse(_configuration["jwt:AccessTokenExpirationMinutes"]
                        ?? throw new ArgumentNullException());

        var refreshTokenExpirationDays = int.Parse(_configuration["jwt:RefreshTokenExpirationDays"]
                        ?? throw new ArgumentNullException());
        
        var refreshTokenExpirationTime = DateTime.UtcNow.AddDays(refreshTokenExpirationDays);
        var accessTokenExpirationTime = DateTime.UtcNow.AddMinutes(accessTokenExpirationMinutes);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims, "Bearer"),
            NotBefore = DateTime.UtcNow,
            Expires = accessTokenExpirationTime,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = GenerateRefreshToken();

        return new Token(tokenHandler.WriteToken(token), refreshToken, accessTokenExpirationTime, refreshTokenExpirationTime);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["jwt:Key"] 
        ?? throw new ArgumentNullException());
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        return principal;
    }
    
    public string GenerateRefreshToken()
    {
        return Utils.GenerateRandomString(20);

    }
}