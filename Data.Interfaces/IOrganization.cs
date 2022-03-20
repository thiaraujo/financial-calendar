using Domain.Entities.Entities;

namespace Data.Interfaces;

public interface IOrganization : IBase<Organization>
{
    Task<IEnumerable<Organization>> GetByAccountId(string accountId);
}