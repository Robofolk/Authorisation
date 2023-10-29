namespace AuthorisationMiddleware.Authorisation;

using Microsoft.AspNetCore.Authorization;

public class RegulatorPolicyHandler : AuthorizationHandler<RegulatorRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RegulatorRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == "Org" && c.Value == "Regulator"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}