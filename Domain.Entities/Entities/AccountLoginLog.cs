using Domain.Entities.Shared;
using Domain.Entities.ValueObjects;

namespace Domain.Entities.Entities;

public class AccountLoginLog : BaseEntity
{
    public string AccountId { get; private set; }
    public AccessFrom AccessFrom { get; private set; }
    public DateTime RegistrationDate { get; private set; }

    public AccountLoginLog(string accountId, AccessFrom accessFrom)
    {
        AccountId = accountId;
        AccessFrom = accessFrom;
        RegistrationDate = DateTime.Now;
    }
}