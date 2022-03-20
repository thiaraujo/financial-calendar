using Domain.Entities.ValueObjects;
using MediatR;

namespace Application.Core.Commands.Authentication;

public class CreateAuthenticationLogCommand : IRequest<bool>
{
    public string AccountId { get; set; } = string.Empty;
    public AccessFrom AccessFrom { get; set; } = new AccessFrom(string.Empty, string.Empty, string.Empty);
}