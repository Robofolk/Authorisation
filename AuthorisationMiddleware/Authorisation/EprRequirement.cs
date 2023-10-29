namespace AuthorisationMiddleware.Authorisation;

using Microsoft.AspNetCore.Authorization;

public class EprRequirement : IAuthorizationRequirement
{
    public EprRequirement() { }
}