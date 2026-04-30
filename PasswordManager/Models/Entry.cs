using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PasswordManager.Models
{
    public class Entry
    {
        public int EntryId { get; set; }

        public int AccountId { get; set; }
        [ValidateNever]
        public Account Account { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a url.")]
        [Url]
        public string Hostname { get; set; } = "";

        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; } = "";

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = "";

        public string Username { get; set; } = "";

        [ValidateNever]
        public List<SecurityQuestion> SecurityQuestions { get; set; } = new();
    }
}