using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Migrations
{
    /// <inheritdoc />
    public partial class sdfdsfa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Points_Users_UserId",
                table: "Points");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Points",
                newName: "QuizProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Points_UserId",
                table: "Points",
                newName: "IX_Points_QuizProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Points_QuizProfile_QuizProfileId",
                table: "Points",
                column: "QuizProfileId",
                principalTable: "QuizProfile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Points_QuizProfile_QuizProfileId",
                table: "Points");

            migrationBuilder.RenameColumn(
                name: "QuizProfileId",
                table: "Points",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Points_QuizProfileId",
                table: "Points",
                newName: "IX_Points_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Points_Users_UserId",
                table: "Points",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
