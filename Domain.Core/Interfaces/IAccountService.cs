using Domain.Core.Dto;

namespace Domain.Core.Interfaces;

public interface IAccountService
{
    Task<AccountDto> Create(AccountDto account);
    Task<AccountDto> Update(AccountDto account);
    Task<AccountDto> Get(string id);
}