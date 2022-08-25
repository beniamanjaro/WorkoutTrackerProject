using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Equipment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectedWorkoutPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryMuscles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryMuscles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimaryMuscles_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecondaryMuscle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryMuscle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondaryMuscle_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimesPerWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutPlans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutPlanId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayOrderNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routines_WorkoutPlans_WorkoutPlanId",
                        column: x => x.WorkoutPlanId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompletedRoutines",
                columns: table => new
                {
                    CompletedRoutineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoutineId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedRoutines", x => x.CompletedRoutineId);
                    table.ForeignKey(
                        name: "FK_CompletedRoutines_Routines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompletedRoutines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoutineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutSets_Routines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    NumberOfReps = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    WorkoutSetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sets_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sets_WorkoutSets_WorkoutSetId",
                        column: x => x.WorkoutSetId,
                        principalTable: "WorkoutSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Category", "Equipment", "Name" },
                values: new object[,]
                {
                    { 1, "Chest", "Bench, Barbell", "Bench Press" },
                    { 2, "Chest", "Cable Machine", "Cable Crossover" },
                    { 3, "Chest", "Bench, Dumbbells", "Dumbbell Flies" },
                    { 4, "Chest", "Bench, Dumbbells", "Dumbbell Press" },
                    { 5, "Chest", "Bench, Barbell", "Incline Benchpress" },
                    { 6, "Chest", "Bench, Dumbbells", "Incline Dumbbell Press" },
                    { 8, "Legs", "Barbell", "Squat" },
                    { 9, "Legs", "", "Calf Raises" },
                    { 10, "Legs", "Barbell", "Front Squat" },
                    { 11, "Legs", "Leg Curls Machine", "Leg Curls" },
                    { 12, "Legs", "Leg Extension Machine", "Leg Extensions" },
                    { 13, "Legs", "Leg Press Machine", "Leg Press" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "SelectedWorkoutPlanId", "Username" },
                values: new object[] { 1, "email@gmail.com", "test123", 0, "test user" });

            migrationBuilder.InsertData(
                table: "PrimaryMuscles",
                columns: new[] { "Id", "ExerciseId", "Name" },
                values: new object[] { 1, 1, "Chest" });

            migrationBuilder.InsertData(
                table: "WorkoutPlans",
                columns: new[] { "Id", "Name", "TimesPerWeek", "UserId" },
                values: new object[] { 1, "Push Pull Legs", 6, 1 });

            migrationBuilder.InsertData(
                table: "Routines",
                columns: new[] { "Id", "DayOrderNumber", "Name", "WorkoutPlanId" },
                values: new object[] { 1, 1, "Chest day", 1 });

            migrationBuilder.InsertData(
                table: "WorkoutSets",
                columns: new[] { "Id", "RoutineId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "WorkoutSets",
                columns: new[] { "Id", "RoutineId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "Sets",
                columns: new[] { "Id", "ExerciseId", "NumberOfReps", "Weight", "WorkoutSetId" },
                values: new object[,]
                {
                    { 1, 1, 8, 80, 1 },
                    { 2, 1, 8, 70, 1 },
                    { 3, 1, 8, 60, 1 },
                    { 4, 2, 5, 55, 2 },
                    { 5, 2, 5, 45, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedRoutines_RoutineId",
                table: "CompletedRoutines",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedRoutines_UserId",
                table: "CompletedRoutines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryMuscles_ExerciseId",
                table: "PrimaryMuscles",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Routines_WorkoutPlanId",
                table: "Routines",
                column: "WorkoutPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryMuscle_ExerciseId",
                table: "SecondaryMuscle",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_ExerciseId",
                table: "Sets",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_WorkoutSetId",
                table: "Sets",
                column: "WorkoutSetId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_UserId",
                table: "WorkoutPlans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSets_RoutineId",
                table: "WorkoutSets",
                column: "RoutineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedRoutines");

            migrationBuilder.DropTable(
                name: "PrimaryMuscles");

            migrationBuilder.DropTable(
                name: "SecondaryMuscle");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "WorkoutSets");

            migrationBuilder.DropTable(
                name: "Routines");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
