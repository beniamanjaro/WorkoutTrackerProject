using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Infrastructure.Migrations
{
    public partial class AddedCompletedRoutinesToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedRoutines_Users_UserId",
                table: "CompletedRoutines");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CompletedRoutines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedRoutines_Users_UserId",
                table: "CompletedRoutines",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedRoutines_Users_UserId",
                table: "CompletedRoutines");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CompletedRoutines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedRoutines_Users_UserId",
                table: "CompletedRoutines",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
