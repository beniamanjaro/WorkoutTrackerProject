using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Infrastructure.Migrations
{
    public partial class changedExerciseModelAndRemovedPrimarySecondaryMuscle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrimaryMuscles");

            migrationBuilder.DropTable(
                name: "SecondaryMuscle");

            migrationBuilder.AddColumn<string>(
                name: "Muscle",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Muscle",
                table: "Exercises");

            migrationBuilder.CreateTable(
                name: "PrimaryMuscles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryMuscles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimaryMuscles_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SecondaryMuscle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryMuscle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondaryMuscle_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryMuscles_ExerciseId",
                table: "PrimaryMuscles",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryMuscle_ExerciseId",
                table: "SecondaryMuscle",
                column: "ExerciseId");
        }
    }
}
