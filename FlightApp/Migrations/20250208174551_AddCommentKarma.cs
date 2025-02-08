using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentKarma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Karma",
                table: "Replies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Karma",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Karma",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "Karma",
                table: "Notes");
        }
    }
}
