using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
  public class Entry
  {
    public int EntryId { get; set; }

    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a url.")]
    public string Hostname { get; set; } = "";


    public string Password { get; set; } = "";

    public string Email { get; set; } = "";

    public string Username { get; set; } = "";

    public List<SecurityQuestion> SecurityQuestions { get; } = new();
  }
}