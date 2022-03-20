using Domain.Entities.Entities;

namespace Data.Interfaces;

public interface IAccount : IBase<Account>
{
    Task<Account> GetAccountByEmail(string address);
}