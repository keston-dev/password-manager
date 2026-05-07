using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using PasswordManager.Models;
using PasswordManager.Services;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers;

[Authorize]
[Route("entries")]
public class EntriesController : BaseController
{
    private readonly EncryptionService _encryption;
    
    public EntriesController(AccountContext ctx, EncryptionService encryption): base(ctx)
    {
        _encryption = encryption;
    }

    [Route("{accountId}")]
    public IActionResult Index(int accountId)
    {
        var account = GetOrCreateAccount();
        return View(account);
    }

    [Route("view/{entryId}")]
    public IActionResult View(int entryId)
    {
        var account = GetOrCreateAccount();

        Entry? active = account.Entries
          .FirstOrDefault(e => e.EntryId == entryId);

        if (active == null) return RedirectToAction("Index", "Home");
        
        var decryptedPassword = _encryption.Decrypt(active.Password);
        
        var reuseCount = account.Entries
            .Where(e => e.EntryId != active.EntryId && !string.IsNullOrEmpty(e.Password))
            .Count(e => _encryption.Decrypt(e.Password).Equals(decryptedPassword));
        
        
        active.Password = decryptedPassword;
        

        return View(new EntryViewModel { Account = account, ActiveEntry = active, PasswordReuseCount = reuseCount });
    }

    [HttpGet("add/{accountId}")]
    public IActionResult Add(int accountId)
    {
        var account = GetOrCreateAccount();

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
        var account = GetOrCreateAccount();
        if (!ModelState.IsValid)
        {
            model.Entry.Account = account;
            model.Action = "Add";
            model.Entries = account.Entries.OrderBy(e => e.EntryId).ToList();
            return View("Edit", model);
        }

        model.Entry.AccountId = account.AccountId;
        model.Entry.Password = _encryption.Encrypt(model.Entry.Password);
        Context.Entries.Add(model.Entry);
        Context.SaveChanges();
        return RedirectToAction("View", new { entryId = model.Entry.EntryId });
    }

    [HttpGet("edit/{entryId}")]
    public IActionResult Edit(int entryId)
    {
        Entry? entry = Context.Entries
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
            var account = GetOrCreateAccount();
            model.Entry.Account = account;
            model.Action = "Edit";
            model.Entries = account.Entries.OrderBy(e => e.EntryId).ToList();
            return View(model);
        }
        model.Entry.Password = _encryption.Encrypt(model.Entry.Password);
        Context.Entries.Update(model.Entry);
        Context.SaveChanges();
        return RedirectToAction("View", new { entryId = model.Entry.EntryId });
    }


    [HttpPost("delete")]
    public IActionResult Delete(int entryId)
    {
        Entry? entry = Context.Entries.Find(entryId);
        if (entry != null)
        {
            int accountId = entry.AccountId;
            Context.Entries.Remove(entry);
            Context.SaveChanges();
            return RedirectToAction("Index", new { accountId });
        }
        return RedirectToAction("Index", "Home");
    }

}