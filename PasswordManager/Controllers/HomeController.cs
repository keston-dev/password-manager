using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using PasswordManager.Models;

namespace PasswordManager.Controllers;

public class HomeController : Controller
{

  private AccountContext context { get; set; }

  private readonly ILogger<HomeController> _logger;


  public HomeController(ILogger<HomeController> logger, AccountContext ctx)
  {
    context = ctx;
    _logger = logger;
  }

  public IActionResult Index(int? id = null)
  {
    ViewBag.ActiveUser = id;
    List<Account> accounts = context.Accounts.OrderBy(a => a.Username).ToList();
    return View(accounts);
  }


  [HttpPost]
  public IActionResult Login(int id, string password)
  {
    _logger.LogInformation($"Password: {password}");
    Account? account = context.Accounts.Find(id);

    // Obviously, at some point, the master password would need to be hashed.
    // Since im just working through basic pre-seeded data at the moment, I won't.


    if (account == null || account.MasterPassword != password)
    {
      TempData.Add("Errors", "Invalid password.");
      return RedirectToAction("Index", new { id });
    }


    return RedirectToAction("Index", "Entries", new { username = account.Username });
  }

  public IActionResult Privacy()
  {
    return View();
  }

}
