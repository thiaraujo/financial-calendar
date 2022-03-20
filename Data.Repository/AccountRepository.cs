using Data.Infra.Context;
using Data.Interfaces;
using Domain.Entities.Entities;
using MongoDB.Driver;

namespace Data.Repository;

public class AccountRepository : BaseRepository<Account>, IAccount
{
    public AccountRepository(IMongoContext context) : base(context)
    {
    }

    public async Task<Account> GetAccountByEmail(string address)
    {
        var filter = Builders<Account>.Filter.Eq(x => x.Email.Address, address.ToLower());
        var account = await Get(filter);
        return account;
    }
}