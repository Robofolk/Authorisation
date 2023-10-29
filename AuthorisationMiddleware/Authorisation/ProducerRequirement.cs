namespace AuthorisationMiddleware.Authorisation;

using Microsoft.AspNetCore.Authorization;

public class ProducerRequirement : IAuthorizationRequirement
{
    public ProducerRequirement() { }
}