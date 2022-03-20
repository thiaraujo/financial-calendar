using Application.Core.Commands.Organization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.API.Abstract;
using WebApi.API.ViewModels;

namespace WebApi.API.Controllers
{
    [Authorize]
    [Route("v1/[controller]")]
    [ApiController]
    public class OrganizationController : AbstractController
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationVm data)
        {
            // Convert data to a command
            var command = new CreateOrganizationCommand
            {
                AccountId = AccountId,
                Identification = data.Identification,
                Image = data.Image,
                OrganizationType = data.OrganizationType
            };

            var response = await _mediator.Send(command);
            return response == null ? BadRequest() : Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var command = new GetOrganizationByIdComand { AccountId = AccountId, OrganizationId = id };

            var response = await _mediator.Send(command);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetOrganizationByAccountCommand { AccountId = AccountId };

            var response = await _mediator.Send(command);
            return response == null ? NotFound() : Ok(response);
        }
    }
}
