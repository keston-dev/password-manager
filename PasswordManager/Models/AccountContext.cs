
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.Models
{
  public class AccountContext : DbContext
  {
    public DbSet<Account> Accounts { get; set; } = null!;

    public DbSet<Entry> Entries { get; set; } = null!;

    public DbSet<SecurityQuestion> SecurityQuestions { get; set; } = null!;

    public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);


      modelBuilder.Entity<Entry>()
        .HasMany(e => e.SecurityQuestions)
        .WithMany(sq => sq.Entries)
        .UsingEntity(j => j.ToTable("EntrySecurityQuestion"));

      modelBuilder.Entity<SecurityQuestion>()
          .HasOne(sq => sq.Account)
          .WithMany(a => a.SecurityQuestions)
          .HasForeignKey(sq => sq.AccountId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<Entry>()
          .HasOne(e => e.Account)
          .WithMany(a => a.Entries)
          .HasForeignKey(e => e.AccountId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<Entry>()
              .HasMany(e => e.SecurityQuestions)
              .WithMany(sq => sq.Entries)
              .UsingEntity<Dictionary<string, object>>(
                  "EntrySecurityQuestion",
                  j => j.HasOne<SecurityQuestion>()
                        .WithMany()
                        .OnDelete(DeleteBehavior.NoAction),
                  j => j.HasOne<Entry>()
                        .WithMany()
                        .OnDelete(DeleteBehavior.Cascade)
              );


      modelBuilder.Entity<Account>().HasData(
        new Account
        {
          AccountId = 1,
          Username = "testUser",
          MasterPassword = "testPassword123",
        },
        new Account { AccountId = 2, Username = "SecondUser", MasterPassword = "anotherpassword!" }
      );
    }
  }
}