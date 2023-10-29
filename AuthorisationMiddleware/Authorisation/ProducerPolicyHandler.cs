namespace AuthorisationMiddleware.Authorisation;

using Microsoft.AspNetCore.Authorization;

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