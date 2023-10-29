namespace AuthorisationMiddleware.Authorisation;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

public class EprPolicyHandler : AuthorizationHandler<EprRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EprPolicyHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        EprRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth &&
                                        c.Issuer == "http://contoso.com"))
        {
            // Check user claims principal
        }

        if (_httpContextAccessor.HttpContext != null)
        {
            var routes = _httpContextAccessor.HttpContext.GetRouteData().Values;
            if (routes.TryGetValue("Controller", out var controller) &&
                routes.TryGetValue(nameof(Action), out var action) &&
                controller is "Home" &&
                action is string and "Index")
            {
                context.Succeed(requirement);
            }
        }

        return Task.FromResult(0);
    }
}