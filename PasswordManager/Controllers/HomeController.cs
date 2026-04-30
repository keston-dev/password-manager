using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using PasswordManager.Models;

namespace PasswordManager.Controllers;

public class HomeController : BaseController
{

    public HomeController(AccountContext ctx, AccountContext context) : base(ctx) { }

    [Route("/")]
    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated != true) return View();

        var account = GetOrCreateAccount();
        return RedirectToAction("Index", "Entries", new { accountId = account.AccountId });

    }

}
