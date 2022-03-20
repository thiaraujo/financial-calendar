using Domain.Entities.ValueObjects;

namespace Domain.Core.Dto;

public class AccountDto
{
    public string Id { get; set; }
    public Name Name { get; set; }
    public Email Email { get; set; }
    public string Picture { get; set; }
    public string Password { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsActive { get; set; }

    public AccountDto() { }
}