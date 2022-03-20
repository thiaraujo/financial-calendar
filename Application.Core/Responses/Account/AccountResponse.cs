using Domain.Entities.ValueObjects;

namespace Application.Core.Responses.Account;

public class AccountResponse
{
    public string Id { get; set; }
    public Name Name { get; set; }
    public Email Email { get; set; }
    public string Picture { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsActive { get; set; }
}