using Application.Core.Responses.Account;
using MediatR;

namespace Application.Core.Commands.Account;

public class GetAccountByIdCommand : IRequest<AccountResponse>
{
    public string Id { get; set; } = string.Empty;
}