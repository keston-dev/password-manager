using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using PasswordManager.Models;

namespace PasswordManager.Controllers;

[Route("Entries/{userName}")]
public class EntriesController : Controller
{

  private AccountContext context { get; set; }



  public EntriesController(AccountContext ctx) => context = ctx;

  // more testing of "basic" routing, moreso for checking out the CSS.
  // obviously later when i have actual authentication, session tokens would need to be validated that this user is even logged-in. 
  // (or fetching more tokens if theyre about to expire)
  public IActionResult Index(string? username = null)
  {
    if (username == null)
    {
      return RedirectToAction("Index", "Home");
    }


    Account? account = context.Accounts.FirstOrDefault(a => a.Username == username);

    if (account == null)
    {
      return RedirectToAction("Index", "Home");
    }

    return View(account);
  }



}
