using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers;

[Route("Entries")]
public class EntriesController : Controller
{
  
  
  private AccountContext context { get; set; }
  
  public EntriesController(AccountContext ctx) => context = ctx;
  
  [Route("{accountId}")]
  public IActionResult Index(int accountId)
  {
    Account? account = context.Accounts
      .Include(a => a.Entries)
      .FirstOrDefault(a => a.AccountId == accountId);

    if (account == null) return RedirectToAction("Index", "Home");

    return View(account);
  }

  [Route("/entries/view/{entryId}")]
  public IActionResult View(int entryId)
  {
    Entry? active = context.Entries
      .Include(e => e.Account)
      .ThenInclude(a => a.Entries)
      .FirstOrDefault(e => e.EntryId == entryId);

    if (active == null) return RedirectToAction("Index", "Home");

    return View(new EntryViewModel { Account = active.Account, ActiveEntry = active });
  }
}