using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AuthorisationMiddleware.Authorisation;

public class ProducerPolicyHandler : AuthorizationHandler<ProducerRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ProducerRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == "Org" && c.Value == "Producer"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}