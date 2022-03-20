using Middleware.Security.Models;

namespace Middleware.Security.Interfaces;

public interface ITokenService
{
    string GenerateToken(UserToken user);
}