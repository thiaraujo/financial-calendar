using Domain.Entities.Shared;
using Domain.Entities.ValueObjects;
using Domain.Enums;

namespace Domain.Entities.Entities;

public class Organization : BaseEntity
{
    public string AccountId { get; private set; }
    public Identification Identification { get; private set; }
    public EOrganizationType OrganizationType { get; private set; }
    public string Image { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public bool IsActive { get; private set; }

    public Organization(string accountId, Identification identification, string image, EOrganizationType organizationType)
    {
        AccountId = accountId;
        Identification = identification;
        Image = image;
        OrganizationType = organizationType;
        RegistrationDate = DateTime.Now; // Normal use
        IsActive = true; // Normal use
    }
}