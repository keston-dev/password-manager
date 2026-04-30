using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PasswordManager.Models;

namespace PasswordManager.ViewModels;

public class EntryEditModel
{
    public Entry Entry { get; set; } = new();

    [ValidateNever]
    public List<Entry> Entries { get; set; } = new();

    public string Action { get; set; }

}