using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


  [Route("/")]
  public IActionResult Index()
  {
    var (accounts, _) = this.GetAccountData(null);
    return View(accounts);
  }

  [Route("/accounts/{id}")]
  public IActionResult Account(int id)
  {
    var (accounts, active) = GetAccountData(id);

    if (active == null) return RedirectToAction("Index");

    ViewBag.ActiveUser = active;

    return View(accounts);
  }


  [HttpPost]
  public IActionResult Login(int id, string password)
  {
    Account? account = context.Accounts.Find(id);

    // Obviously, at some point, the master password would need to be hashed.
    // Since im just working through basic pre-seeded data at the moment, I won't.


    if (account == null || account.MasterPassword != password)
    {
      TempData.Add("Errors", "Invalid password.");
      return RedirectToAction("Index", new { id });
    }


    return RedirectToAction("Index", "Entries", new { accountId = account.AccountId });
  }
  



  private (List<Account>, Account? active) GetAccountData(int? id)
  {
    List<Account> accounts = context.Accounts.OrderBy((a) => a.AccountId).ToList();
    Account? active = accounts.FirstOrDefault((a) => a.AccountId == id);
    return (accounts, active);
  }

}
