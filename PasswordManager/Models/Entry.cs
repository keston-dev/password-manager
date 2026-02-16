


namespace PasswordManager.Models
{
  public class Entry
  {
    public int EntryId { get; set; }

    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;

    public string Hostname { get; set; } = "";


    public string Password { get; set; } = "";

    public string Email { get; set; } = "";

    public string Username { get; set; } = "";

    public List<EntrySecurityQuestion> EntrySecurityQuestions { get; } = new();
  }
}