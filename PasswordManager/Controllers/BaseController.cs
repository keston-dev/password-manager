using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;

namespace PasswordManager.Controllers;

public class BaseController : Controller
{
    protected readonly AccountContext Context;

    public BaseController(AccountContext ctx) => Context = ctx;

    protected Account? GetCurrentAccount()
    {
        var googleId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (googleId == null) return null;

        return Context.Accounts.FirstOrDefault(a => a.GoogleId == googleId);
    }

    protected Account GetOrCreateAccount()
    {
        var googleId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        var account = Context.Accounts
            .Include(a => a.Entries)
            .ThenInclude(e => e.SecurityQuestions)
            .FirstOrDefault(a => a.GoogleId == googleId);

        if (account == null)
        {
            account = new Account { GoogleId = googleId };
            Context.Accounts.Add(account);
            Context.SaveChanges();
        }

        return account;
    }
}