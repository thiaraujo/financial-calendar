using Application.Core.Responses.Authentication;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using MediatR;

namespace Application.Core.Commands.Authentication;

public class CreateAuthenticationCommand : IRequest<AuthenticationResponse>
{
    public Email Email { get; set; } = new Email(string.Empty, ELoginType.SelfAccount);
    public string Password { get; set; } = string.Empty;
}