using Domain.Entities.ValueObjects;
using Domain.Enums;

namespace Application.Core.Responses.Organization;

public class OrganizationResponse
{
    public string Id { get; set; }
    public Identification Identification { get; set; }
    public EOrganizationType OrganizationType { get; set; }
    public string Image { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsActive { get; set; }
}