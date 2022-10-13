using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Infrastructure.Migrations
{
    public partial class AddedCompletedRoutine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedRoutines_Routines_RoutineId",
                table: "CompletedRoutines");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedRoutines_Users_UserId",
                table: "CompletedRoutines");

            migrationBuilder.DropIndex(
                name: "IX_CompletedRoutines_RoutineId",
                table: "CompletedRoutines");

            migrationBuilder.RenameColumn(
                name: "RoutineId",
                table: "CompletedRoutines",
                newName: "TotalVolume");

            migrationBuilder.AddColumn<int>(
                name: "TotalReps",
                table: "CompletedRoutines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSets",
                table: "CompletedRoutines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CompletedRoutineExercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletedRoutineId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedRoutineExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedRoutineExercise_CompletedRoutines_CompletedRoutineId",
                        column: x => x.CompletedRoutineId,
                        principalTable: "CompletedRoutines",
                        principalColumn: "CompletedRoutineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompletedRoutineExercise_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedRoutineExercise_CompletedRoutineId",
                table: "CompletedRoutineExercise",
                column: "CompletedRoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedRoutineExercise_ExerciseId",
                table: "CompletedRoutineExercise",
                column: "ExerciseId");

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

            migrationBuilder.DropTable(
                name: "CompletedRoutineExercise");

            migrationBuilder.DropColumn(
                name: "TotalReps",
                table: "CompletedRoutines");

            migrationBuilder.DropColumn(
                name: "TotalSets",
                table: "CompletedRoutines");

            migrationBuilder.RenameColumn(
                name: "TotalVolume",
                table: "CompletedRoutines",
                newName: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedRoutines_RoutineId",
                table: "CompletedRoutines",
                column: "RoutineId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedRoutines_Routines_RoutineId",
                table: "CompletedRoutines",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedRoutines_Users_UserId",
                table: "CompletedRoutines",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
