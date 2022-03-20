using AutoMapper;
using Domain.Core.Dto;
using Domain.Core.Interfaces;

namespace Domain.Core.Services;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;

    public AccountService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<AccountDto> Create(AccountDto account)
    {
        throw new NotImplementedException();
    }

    public Task<AccountDto> Update(AccountDto account)
    {
        throw new NotImplementedException();
    }

    public Task<AccountDto> Get(string id)
    {
        throw new NotImplementedException();
    }
}