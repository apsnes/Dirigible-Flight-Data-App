using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlightApp.Migrations
{
    /// <inheritdoc />
    public partial class seedusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "FirstName", "Karma", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Pronouns", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "06ed9993-e27e-42b6-a8bb-6ba7eeca5eb5", 0, "1c38c638-3a39-4476-bc78-c3f6c2f9f624", null, "admin@example.com", true, "Hello", 0, "Hello", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKY7F0cAWyfwE7+zlk2ULxUlw6K+/zEHdfcDu5SEvnjaEeADMWtXwccoycbRC8QdBA==", "077123123123", false, null, "", false, "admin@example.com" },
                    { "805e790a-5ae4-46b2-aec5-fa540b153f5d", 0, "9a62d086-5133-44df-9892-be328e97a3a7", null, "customer@example.com", true, "Hello", 0, "Hello", false, null, "CUSTOMER@EXAMPLE.COM", "CUSTOMER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGrzF5Cxs+ge3N+voEFuxR+1Dw2fPF6j9onDeEAO1vU83yjNaE+0KGWIgf2miQF++w==", "077123123123", false, null, "", false, "customer@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "Role", "UserId" },
                values: new object[,]
                {
                    { "1", "06ed9993-e27e-42b6-a8bb-6ba7eeca5eb5" },
                    { "2", "805e790a-5ae4-46b2-aec5-fa540b153f5d" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "Role", "UserId" },
                keyValues: new object[] { "1", "06ed9993-e27e-42b6-a8bb-6ba7eeca5eb5" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "Role", "UserId" },
                keyValues: new object[] { "2", "805e790a-5ae4-46b2-aec5-fa540b153f5d" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "06ed9993-e27e-42b6-a8bb-6ba7eeca5eb5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "805e790a-5ae4-46b2-aec5-fa540b153f5d");
        }
    }
}
