using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
  public class Account
  {
    public int AccountId { get; set; }

    [Required(ErrorMessage = "Please enter a username.")]
    public string Username { get; set; } = "";

    // using the term "master password" to distingush from a regular stored password.
    // This would be used to bar entry from accessing the entries.
    [Required(ErrorMessage = "Please enter a master password.")]
    public string MasterPassword { get; set; } = "";

    public List<Entry> Entries { get; } = new();

    public List<SecurityQuestion> SecurityQuestions { get; } = new();
  }
}