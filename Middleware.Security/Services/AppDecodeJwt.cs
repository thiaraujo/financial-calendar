using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Middleware.Security.Services
{
    public class AppDecodeJwt
    {
        public static string DecodeTokenText(HttpContext httpContext, string key)
        {
            var header = httpContext.Request.Headers["Authorization"];
            if (header.Count < 1)
                return null;

            var jwtEncodedString = header.ToString().Substring(7);
            var decode = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            var value = decode.Claims.FirstOrDefault(x => x.Type == key);

            return value?.Value;
        }
    }
}
