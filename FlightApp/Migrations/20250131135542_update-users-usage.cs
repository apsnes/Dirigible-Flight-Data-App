using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightApp.Migrations
{
    /// <inheritdoc />
    public partial class updateusersusage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightNotes_Notes_FlightNumber",
                table: "FlightNotes");

            migrationBuilder.DropIndex(
                name: "IX_FlightNotes_FlightNumber",
                table: "FlightNotes");

            migrationBuilder.DropColumn(
                name: "FlightNumber",
                table: "FlightNotes");

            migrationBuilder.CreateIndex(
                name: "IX_FlightNotes_NoteId",
                table: "FlightNotes",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightNotes_Notes_NoteId",
                table: "FlightNotes",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightNotes_Notes_NoteId",
                table: "FlightNotes");

            migrationBuilder.DropIndex(
                name: "IX_FlightNotes_NoteId",
                table: "FlightNotes");

            migrationBuilder.AddColumn<int>(
                name: "FlightNumber",
                table: "FlightNotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlightNotes_FlightNumber",
                table: "FlightNotes",
                column: "FlightNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightNotes_Notes_FlightNumber",
                table: "FlightNotes",
                column: "FlightNumber",
                principalTable: "Notes",
                principalColumn: "Id");
        }
    }
}
