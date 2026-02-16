


namespace PasswordManager.Models
{
  public class Account
  {
    public int AccountId { get; set; }

    public string Username { get; set; } = "";

    // using the term "master password" to distingush from a regular stored password.
    // This would be used to bar entry from accessing the entries.
    public string MasterPassword { get; set; } = "";

    public List<Entry> Entries { get; } = new();

    public List<SecurityQuestion> SecurityQuestions { get; } = new();
  }
}