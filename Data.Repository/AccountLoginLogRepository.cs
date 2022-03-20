using Data.Infra.Context;
using Data.Interfaces;
using Domain.Entities.Entities;

namespace Data.Repository;

public class AccountLoginLogRepository : BaseRepository<AccountLoginLog>, IAccountLoginLog
{
    public AccountLoginLogRepository(IMongoContext context) : base(context)
    {
    }
}