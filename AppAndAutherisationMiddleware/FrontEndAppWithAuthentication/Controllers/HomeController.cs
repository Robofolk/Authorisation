using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using FrontEndAppWithAuthentication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FrontEndAppWithAuthentication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) => _logger = logger;

    /// [AllowAnonymous]
    [Authorize(Policy="Epr")]
    public IActionResult Index() => View();

    [Authorize]
    public IActionResult Privacy() => View();

    [AllowAnonymous]
    public IActionResult Anybody() => View();

    [Authorize(Policy = "Producer")]
    public IActionResult ProducerAll() => View();
    
    [Authorize(Policy = "Producer", Roles = "Admin")]
    public IActionResult ProducerAdmin() => View();
    
    [Authorize(Policy = "Producer", Roles = "ApprovedPerson")]
    public IActionResult ProducerApprovedPerson() => View();
    
    [Authorize(Policy = "Regulator")]
    public IActionResult Regulator() => View();
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [AllowAnonymous]
    public async Task<IActionResult> SignInRegulator()
    {
        var claims = new List<Claim>
        {
            new("Org", "Regulator"), 
        };
        var claimsIdentity = new ClaimsIdentity(HttpContext.User.Identity, claims);
        var principal = new ClaimsPrincipal(claimsIdentity);
        var properties = HttpContext.Features.Get<IAuthenticateResultFeature>()?.AuthenticateResult?.Properties;

        var test = principal.IsInRole("ApprovedPerson");

        //await HttpContext.SignInAsync(IdentityConstants.ExternalScheme, principal, properties);
        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal, properties);
        //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

        return new RedirectResult("Index");
    }

    [AllowAnonymous]
    public async Task<IActionResult> SignInProducerApprovedPerson()
    {
        var claims = new List<Claim>
        {
            new("Org", "Producer"), 
            new(ClaimTypes.Role, "ApprovedPerson")
        };
        var claimsIdentity = new ClaimsIdentity(HttpContext.User.Identity, claims);
        var principal = new ClaimsPrincipal(claimsIdentity);
        var properties = HttpContext.Features.Get<IAuthenticateResultFeature>()?.AuthenticateResult?.Properties;

        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal, properties);

        return new RedirectResult("Index");
    }

    [AllowAnonymous]
    public async Task<IActionResult> SignInProducerAdmin()
    {
        var claims = new List<Claim>
        {
            new("Org", "Producer"), 
            new(ClaimTypes.Role, "Admin")
        };
        var claimsIdentity = new ClaimsIdentity(HttpContext.User.Identity, claims);
        var principal = new ClaimsPrincipal(claimsIdentity);
        var properties = HttpContext.Features.Get<IAuthenticateResultFeature>()?.AuthenticateResult?.Properties;

        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal, properties);

        return new RedirectResult("Index");
    }
}