using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers;

[Route("entries")]
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

  [Route("view/{entryId}")]
  public IActionResult View(int entryId)
  {
    Entry? active = context.Entries
      .Include(e => e.Account)
      .ThenInclude(a => a.Entries)
      .FirstOrDefault(e => e.EntryId == entryId);

    if (active == null) return RedirectToAction("Index", "Home");

    return View(new EntryViewModel { Account = active.Account, ActiveEntry = active });
  }
  
    [HttpGet("add/{accountId}")]
    public IActionResult Add(int accountId)
    {
        Account? account = context.Accounts
            .Include(a => a.Entries)
            .FirstOrDefault(a => a.AccountId == accountId);

        if (account == null) return RedirectToAction("Index", "Home");

        return View("Edit", new EntryEditModel
        {
            Entry = new Entry { AccountId = accountId, Account = account },
            Action = "Add",
            Entries = account.Entries.OrderBy(e => e.EntryId).ToList()
        });
    }

    [HttpPost("add/{accountId}")]
    public IActionResult Add(EntryEditModel model, int accountId) 
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .Select(x => $"{x.Key}: {x.Value!.Errors[0].ErrorMessage}");
            // Set a breakpoint here or log `errors`
            foreach (var e in errors) Console.WriteLine(e);
            var account = context.Accounts
                .Include(a => a.Entries)
                .First(a => a.AccountId == accountId);
            model.Entry.Account = account;
            model.Action = "Add";
            model.Entries = account.Entries.OrderBy(e => e.EntryId).ToList();
            return View("Edit", model);
        }
        context.Entries.Add(model.Entry);
        context.SaveChanges();
        return RedirectToAction("View", new { entryId = model.Entry.EntryId });
    }

    [HttpGet("edit/{entryId}")]
    public IActionResult Edit(int entryId)
    {
        Entry? entry = context.Entries
            .Include(e => e.Account)
            .ThenInclude(a => a.Entries)
            .Include(e => e.SecurityQuestions)
            .FirstOrDefault(e => e.EntryId == entryId);

        if (entry == null) return RedirectToAction("Index", "Home");

        return View(new EntryEditModel
        {
            Entry = entry,
            Action = "Edit",
            Entries = entry.Account.Entries.OrderBy(e => e.EntryId).ToList()
        });
    }

    [HttpPost("edit")]
    public IActionResult Edit(EntryEditModel model)
    {
        if (!ModelState.IsValid)
        {
            var account = context.Accounts
                .Include(a => a.Entries)
                .First(a => a.AccountId == model.Entry.AccountId);
            model.Entry.Account = account;
            model.Action = "Edit";
            model.Entries = account.Entries.OrderBy(e => e.EntryId).ToList();
            return View(model);
        }
        context.Entries.Update(model.Entry);
        context.SaveChanges();
        return RedirectToAction("View", new { entryId = model.Entry.EntryId });
    }   


  [HttpPost("delete")]
  public IActionResult Delete(int entryId)
  {
    Entry? entry = context.Entries.Find(entryId);
    if (entry != null)
    {
      int accountId = entry.AccountId;
      context.Entries.Remove(entry);
      context.SaveChanges();
      return RedirectToAction("Index", new { accountId });
    }
    return RedirectToAction("Index", "Home");
  }

}