using Microsoft.AspNetCore.Authorization;

namespace AuthorisationMiddleware.Authorisation;

public class EprRequirement : IAuthorizationRequirement
{
    public EprRequirement() { }
}