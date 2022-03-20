using Application.Core.Responses.Organization;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using MediatR;

namespace Application.Core.Commands.Organization
{
    public class CreateOrganizationCommand : IRequest<OrganizationResponse>
    {
        public string AccountId { get; set; } = string.Empty;
        public Identification Identification { get; set; } = new Identification(string.Empty, string.Empty);
        public EOrganizationType OrganizationType { get; set; } = EOrganizationType.Personal;
        public string Image { get; set; } = string.Empty;
    }
}
