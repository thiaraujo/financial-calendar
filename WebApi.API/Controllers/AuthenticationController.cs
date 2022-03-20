using Application.Core.Commands.Account;
using Application.Core.Commands.Authentication;
using Application.Core.Responses.Authentication;
using Domain.Entities.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middleware.Extensions;
using Middleware.Security.Interfaces;
using Middleware.Security.Models;
using WebApi.API.Abstract;

namespace WebApi.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthenticationController : AbstractController
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IMediator mediator, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
        }

        // Create account for user then create a authentication and return JWT
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == null) return BadRequest();

            //If created then authenticate user
            var commandAuth = new AuthenticationResponse
            {
                Id = response.Id,
                Email = new Email(response.Email.Address, response.Email.LoginType)
            };

            // create token
            var token = _tokenService.GenerateToken(new UserToken
            {
                ELoginType = command.Email.LoginType,
                Email = command.Email.Address,
                Id = response.Id
            });
            commandAuth.JwtToken = token; //set token in response

            return commandAuth.JwtToken.IsNull() ? BadRequest() : Ok(commandAuth);
        }

        // Only do a login
        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return Unauthorized();

            var command = new CreateAuthenticationCommand
            {
                Email = new Email(user.Email.ToLower(), user.ELoginType),
                Password = user.Password
            };

            var response = await _mediator.Send(command);

            // login or password incorrect
            if (response == null)
                return Unauthorized();

            // create token
            var token = _tokenService.GenerateToken(new UserToken
            {
                ELoginType = command.Email.LoginType,
                Email = command.Email.Address,
                Id = response.Id
            });
            response.JwtToken = token; //set token in resonse

            // Send to function for create a new log from login user
            await CreateLoginLog(response.Id, user);

            return Ok(response);
        }

        // Function to create log from access user
        private async Task CreateLoginLog(string accountId, User user)
        {
            // Create the log
            var logCommand = new CreateAuthenticationLogCommand
            {
                AccountId = accountId,
                AccessFrom = new AccessFrom(user.From.Device, user.From.Ip, user.From.City),
            };
            await _mediator.Send(logCommand);
        }
    }
}
