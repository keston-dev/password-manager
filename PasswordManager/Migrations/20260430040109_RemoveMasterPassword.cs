using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PasswordManager.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMasterPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Entries",
                keyColumn: "EntryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Entries",
                keyColumn: "EntryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "MasterPassword",
                table: "Accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MasterPassword",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "GoogleId", "MasterPassword", "Username" },
                values: new object[,]
                {
                    { 1, null, "testPassword123", "testUser" },
                    { 2, null, "anotherpassword!", "SecondUser" }
                });

            migrationBuilder.InsertData(
                table: "Entries",
                columns: new[] { "EntryId", "AccountId", "Email", "Hostname", "Password", "Username" },
                values: new object[,]
                {
                    { 1, 1, "", "google.com", "example", "testUser" },
                    { 2, 1, "", "github.com", "example123!", "testUser" }
                });
        }
    }
}
