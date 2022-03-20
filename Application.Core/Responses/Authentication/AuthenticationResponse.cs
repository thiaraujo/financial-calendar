using Domain.Entities.ValueObjects;

namespace Application.Core.Responses.Authentication;

public class AuthenticationResponse
{
    public string Id { get; set; }
    public Name Name { get; set; }
    public Email Email { get; set; }
    public string JwtToken { get; set; }
}