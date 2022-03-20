using Microsoft.AspNetCore.Mvc;
using Middleware.Security.Services;

namespace WebApi.API.Abstract;

public class AbstractController : ControllerBase
{
    public string AccountId => HttpContext.AccountId();
    public string AccountEmail => HttpContext.AccountEmail();
}