using Application.Core.Responses.Organization;
using MediatR;

namespace Application.Core.Commands.Organization;

public class GetOrganizationByAccountCommand : IRequest<IEnumerable<OrganizationResponse>>
{
    public string AccountId { get; set; } = string.Empty;
}