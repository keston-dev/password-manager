using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Models;

namespace PasswordManager.Controllers;

[Route("auth")]
public class AuthenticationController : BaseController
{

    public AuthenticationController(AccountContext context) : base(context) { }


    [HttpGet("login")]
    public IActionResult Login() => Challenge(new AuthenticationProperties { RedirectUri = "/auth/post-login" }, GoogleDefaults.AuthenticationScheme);


    [Authorize]
    [HttpGet("post-login")]
    public IActionResult PostLogin()
    {
        var account = GetOrCreateAccount();
        return RedirectToAction("Index", "Entries", new { accountId = account.AccountId });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}