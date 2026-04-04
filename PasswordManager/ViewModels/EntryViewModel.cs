using PasswordManager.Models;

namespace PasswordManager.ViewModels;

public class EntryViewModel
{
    public Account Account { get; set; }
    public Entry ActiveEntry { get; set; }
}