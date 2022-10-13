using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Infrastructure.Migrations
{
    public partial class addedWorkoutPlanIdToCompletedRoutine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkoutPlan_WorkoutPlans_WorkoutPlansId",
                table: "UserWorkoutPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_Users_UserId",
                table: "WorkoutPlans");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPlans_UserId",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "WorkoutPlans");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutPlanId",
                table: "CompletedRoutines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkoutPlan_WorkoutPlans_WorkoutPlansId",
                table: "UserWorkoutPlan",
                column: "WorkoutPlansId",
                principalTable: "WorkoutPlans",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkoutPlan_WorkoutPlans_WorkoutPlansId",
                table: "UserWorkoutPlan");

            migrationBuilder.DropColumn(
                name: "WorkoutPlanId",
                table: "CompletedRoutines");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "WorkoutPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_UserId",
                table: "WorkoutPlans",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkoutPlan_WorkoutPlans_WorkoutPlansId",
                table: "UserWorkoutPlan",
                column: "WorkoutPlansId",
                principalTable: "WorkoutPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_Users_UserId",
                table: "WorkoutPlans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
