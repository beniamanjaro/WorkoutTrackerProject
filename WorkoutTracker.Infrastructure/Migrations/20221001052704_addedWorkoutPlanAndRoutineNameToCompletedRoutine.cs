using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Infrastructure.Migrations
{
    public partial class addedWorkoutPlanAndRoutineNameToCompletedRoutine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoutineName",
                table: "CompletedRoutines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkoutPlanName",
                table: "CompletedRoutines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoutineName",
                table: "CompletedRoutines");

            migrationBuilder.DropColumn(
                name: "WorkoutPlanName",
                table: "CompletedRoutines");
        }
    }
}
