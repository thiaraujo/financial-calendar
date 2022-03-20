using Microsoft.AspNetCore.Http;

namespace Middleware.Security.Services;

public static class ExtractJwtToken
{
    public static string AccountId(this HttpContext context)
    {
        return AppDecodeJwt.DecodeTokenText(context, "Id");
    }

    public static string AccountEmail(this HttpContext context)
    {
        return AppDecodeJwt.DecodeTokenText(context, "Email");
    }
}