namespace AuthorisationMiddleware.ConfigurationExtensions;

using Authorisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static void RegisterAuthorisationService(this IServiceCollection services)
    {
        services
            .AddHttpContextAccessor()
            // .AddAuthenticationCore(options =>
            // {
            //     options.DefaultAuthenticateScheme = string.Empty; //CookieAuthenticationDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = string.Empty; //CookieAuthenticationDefaults.AuthenticationScheme;
            //     options.RequireAuthenticatedSignIn = true;
            // })
            .AddAuthorizationCore(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAssertion(_ => true)
                    .Build();
                
                // set global authorisation for all actions on all controller
                // unless if attributed by [AllowAnonymous]
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    //.RequireAuthenticatedUser()
                    .RequireAssertion(_ => true)
                    .Build();
                
                options.AddPolicy("Epr",
                    policy => policy.Requirements.Add(new EprRequirement()));
                
                options.AddPolicy("Producer",
                    policy => policy.Requirements.Add(new ProducerRequirement()));
                
                options.AddPolicy("Regulator",
                    policy => policy.Requirements.Add(new RegulatorRequirement()));

                options.AddPolicy("ApprovedPerson",
                    policy => policy.RequireRole("ApprovedPerson"));
                options.AddPolicy("Admin",
                    policy => policy.RequireRole("Admin"));
            })
            .AddSingleton<IAuthorizationHandler, EprPolicyHandler>()
            .AddSingleton<IAuthorizationHandler, ProducerPolicyHandler>()
            .AddSingleton<IAuthorizationHandler, RegulatorPolicyHandler>();
    }
}