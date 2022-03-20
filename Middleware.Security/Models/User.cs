using Domain.Enums;

namespace Middleware.Security.Models;

public class UserToken
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ELoginType ELoginType { get; set; }
}

public class User
{
    public string Email { get; set; } = string.Empty;
    public ELoginType ELoginType { get; set; }
    public string Password { get; set; } = string.Empty;
    public UserAccessFrom From { get; set; } = new UserAccessFrom();
}

public class UserAccessFrom
{
    public string Device { get; set; } = string.Empty;
    public string Ip { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}