using Microsoft.IdentityModel.Tokens;
using Middleware.Security.Interfaces;
using Middleware.Security.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Middleware.Security.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(UserToken user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = SecretKey.GetKey();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id)
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}