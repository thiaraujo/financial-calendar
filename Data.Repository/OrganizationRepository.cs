using Data.Infra.Context;
using Data.Interfaces;
using Domain.Entities.Entities;
using MongoDB.Driver;

namespace Data.Repository;

public class OrganizationRepository : BaseRepository<Organization>, IOrganization
{
    public OrganizationRepository(IMongoContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Organization>> GetByAccountId(string accountId)
    {
        var filter = Builders<Organization>.Filter.Eq(x => x.AccountId, accountId);
        var account = await GetAll(filter);
        return account;
    }
}