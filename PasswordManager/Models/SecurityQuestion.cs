


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


    public List<Entry> Entries { get; } = new();
  }

}