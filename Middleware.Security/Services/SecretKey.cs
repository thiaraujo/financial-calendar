using Microsoft.Extensions.Configuration;
using System.Text;

namespace Middleware.Security.Services;

public static class SecretKey
{
    public static byte[] GetKey()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

        var key = Encoding.ASCII.GetBytes(configuration["Secret"]);
        return key;
    }
}