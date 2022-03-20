using Application.Core.Responses.Account;
using MediatR;

namespace Application.Core.Commands.Account;

public class GetAccountByEmailCommand : IRequest<AccountResponse>
{
    public string Email { get; set; } = string.Empty;
}