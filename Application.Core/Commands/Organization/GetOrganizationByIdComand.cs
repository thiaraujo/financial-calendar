using Application.Core.Responses.Organization;
using MediatR;

namespace Application.Core.Commands.Organization;

public class GetOrganizationByIdComand : IRequest<OrganizationResponse>
{
    public string AccountId { get; set; } = string.Empty;
    public string OrganizationId { get; set; } = string.Empty;
}