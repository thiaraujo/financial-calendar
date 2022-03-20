using Application.Core.Commands.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.API.Abstract;

namespace WebApi.API.Controllers
{
    [Authorize]
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountController : AbstractController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var command = new GetAccountByIdCommand { Id = AccountId };
            var response = await _mediator.Send(command);
            return response == null ? NotFound() : Ok(response);
        }

        // Get account by email, for validate if is unique account
        [AllowAnonymous]
        [HttpGet("email/{address}")]
        public async Task<IActionResult> GetByEmail(string address)
        {
            var command = new GetAccountByEmailCommand { Email = address };
            var response = await _mediator.Send(command);
            return response == null ? Ok(JsonConvert.SerializeObject(null)) : Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAccountCommand command)
        {
            command.Id = AccountId; //Get Id from user

            var response = await _mediator.Send(command);
            return response == null ? BadRequest() : Ok(response);
        }
    }
}
