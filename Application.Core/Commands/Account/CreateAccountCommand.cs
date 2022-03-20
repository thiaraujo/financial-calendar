using Application.Core.Responses.Account;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using MediatR;

namespace Application.Core.Commands.Account;

public class CreateAccountCommand : IRequest<AccountResponse>
{
    public Name Name { get; set; } = new Name(string.Empty, string.Empty);
    public Email Email { get; set; } = new Email(string.Empty, ELoginType.SelfAccount);
    public string Picture { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}