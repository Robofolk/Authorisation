using Microsoft.AspNetCore.Authorization;

namespace AuthorisationMiddleware.Authorisation;

public class ProducerRequirement : IAuthorizationRequirement
{
    public ProducerRequirement() { }
}