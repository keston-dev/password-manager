


namespace PasswordManager.Models
{
  public class SecurityQuestion
  {
    public int SecurityQuestionId { get; set; }

    public int AccountId { get; set; }

    public Account Account { get; set; } = null!;

    // TODO: preload security questions on migration? Common ones like first pet, city you were born, mother's maiden name, etc.
    public string Question { get; set; } = "";

    public string Answer { get; set; } = "";


    public List<EntrySecurityQuestion> EntrySecurityQuestions { get; } = new();
  }

  public class EntrySecurityQuestion
  {
    public int EntryId { get; set; }
    public Entry Entry { get; set; } = null!;

    public int SecurityQuestionId { get; set; }
    public SecurityQuestion SecurityQuestion { get; set; } = null!;
  }
}