using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        var expiresMinutes = double.Parse(_configuration["jwt:ExpiresMinutes"]);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims, "Bearer"),
            NotBefore = DateTime.UtcNow.AddHours(-3),
            Expires = DateTime.UtcNow.AddHours(-3).AddMinutes(expiresMinutes),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = GenerateRefreshToken();

        return new Token(tokenHandler.WriteToken(token), refreshToken, tokenDescriptor.Expires);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["jwt:Key"]);
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
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
        var randomNumber = new byte[32];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private static List<(string, string)> _refreshTokens = new();

    public static void SaveRefreshToken(string username, string refreshToken)
    {
        _refreshTokens.Add((username, refreshToken));
    }

    public static string GetSavedRefreshToken(string username)
    {
        return  _refreshTokens.FirstOrDefault(x => x.Item1 == username).Item2;
    }

    public static void RemoveRefreshToken(string username, string refreshToken)
    {
        var item  = _refreshTokens.FirstOrDefault(x => x.Item1 == username && x.Item2 == refreshToken);
        _refreshTokens.Remove(item);
    }
}