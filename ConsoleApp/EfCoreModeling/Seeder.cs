using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Infrastructure;
using WorkoutTracker.Infrastructure.Repositories;

namespace ConsoleApp.EfCoreModeling
{
    public class Seeder
    {
       public static async Task SeedData()
        {
            var context = new WorkoutContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            var exercises = GetExercisesPreConfigured().ToList();
            var users = GetPreConfiguredUsers().ToList();
            var workoutPlans = GetPreConfiguredWorkoutPlans(users).ToList();
            var routines = GetPreConfiguredRoutines(workoutPlans).ToList();
            var workoutSets = GetPreConfiguredWorkoutSets(routines).ToList();
            var sets = GetPreConfiguredSets(workoutSets, exercises).ToList();
            var completedRoutines = GetPreConfiguredCompletedRoutines(users, routines).ToList();

            context.Exercises.AddRange(exercises);
            context.Users.AddRange(users);
            context.WorkoutPlans.AddRange(workoutPlans);
            context.Routines.AddRange(routines);
            context.WorkoutSets.AddRange(workoutSets);
            context.Sets.AddRange(sets);
            context.CompletedRoutines.AddRange(completedRoutines);

            await context.SaveChangesAsync();
        }

        public static IEnumerable<Exercise> GetExercisesPreConfigured()
        {
            var exercises = new List<Exercise> {
                new Exercise() {  Name = "Bench Press", Category = "Chest", Equipment = "Bench, Barbell" },
                new Exercise() {  Name = "Cable Crossover", Category = "Chest", Equipment = "Cable Machine" },
                new Exercise() {  Name = "Dumbbell Flies", Category = "Chest", Equipment = "Bench, Dumbbells" },
                new Exercise() {  Name = "Dumbbell Press", Category = "Chest", Equipment = "Bench, Dumbbells" },
                new Exercise() {  Name = "Incline Bench Press", Category = "Chest", Equipment = "Bench, Barbell" },
                new Exercise() {  Name = "Incline Dumbbell Press", Category = "Chest", Equipment = "Bench, Dumbbells" },
                new Exercise() {  Name = "Squat", Category = "Legs", Equipment = "Barbell" },
                new Exercise() {  Name = "Calf Raises", Category = "Legs", Equipment = "" },
                new Exercise() {  Name = "Front Squat", Category = "Legs", Equipment = "Barbell" },
                new Exercise() {  Name = "Leg Curls", Category = "Legs", Equipment = "Leg Curls Machine" },
                new Exercise() {  Name = "Leg Extensions", Category = "Legs", Equipment = "Leg Extension Machine" },
                new Exercise() {  Name = "Leg Press", Category = "Legs", Equipment = "Leg Press Machine" },
                new Exercise() {  Name = "Lunges", Category = "Legs", Equipment = "Barbell" },
                new Exercise() {  Name = "Seated Calf Raises", Category = "Legs", Equipment = "" },
                new Exercise() {  Name = "Dumbbell Lateral Raises", Category = "Shoulders", Equipment = "Dumbbell" },
                new Exercise() {  Name = "Military Press", Category = "Shoulders", Equipment = "Barbell" },
                new Exercise() {  Name = "Shoulder Dumbbell Press", Category = "Shoulders", Equipment = "Dumbbell" },
                new Exercise() {  Name = "Upright Rows", Category = "Shoulders", Equipment = "Barbell" },
                new Exercise() {  Name = "Assited Dips", Category = "Triceps", Equipment = "" },
                new Exercise() {  Name = "Close Grip Bench Press", Category = "Triceps", Equipment = "Barbell Bench" },
                new Exercise() {  Name = "Dips", Category = "Triceps", Equipment = "" },
                new Exercise() {  Name = "Pushdowns", Category = "Triceps", Equipment = "Cable" },
                new Exercise() {  Name = "Assisted Chin Up", Category = "Back", Equipment = "" },
                new Exercise() {  Name = "Assisted Pull Up", Category = "Back", Equipment = "" },
                new Exercise() {  Name = "Pull up", Category = "Back", Equipment = "" },
                new Exercise() {  Name = "Chin Up", Category = "Back", Equipment = "" },
                new Exercise() {  Name = "Barbell Row", Category = "Back", Equipment = "Barbell" },
                new Exercise() {  Name = "Cable Row", Category = "Back", Equipment = "Cable" },
                new Exercise() {  Name = "Deadlift", Category = "Back", Equipment = "Barbell" },
                new Exercise() {  Name = "Dumbbell Row", Category = "Back", Equipment = "Dumbbell" },
                new Exercise() {  Name = "Hyperextensions", Category = "Back", Equipment = "" },
                new Exercise() {  Name = "Pulldowns", Category = "Back", Equipment = "Cable" }
                };
            return exercises;
        }

        public static IEnumerable<User> GetPreConfiguredUsers()
        {
            return new List<User> { new User { Username = "Jeff", Email = "jeff@yahoo.com" }, new User { Username = "benogre", Email = "benogre@yahoo.com" } };
        }

        public static IEnumerable<WorkoutPlan> GetPreConfiguredWorkoutPlans (List<User> users)
        {

            var rnd = new Random();

          

            var fakeWorkoutPlans = Enumerable
                .Range(1, rnd.Next(3, 6))
                .Select(_ => new Faker<WorkoutPlan>()
                .RuleFor(workoutPlan => workoutPlan.User, faker => faker.PickRandom(users))
                .RuleFor(workoutPlan => workoutPlan.TimesPerWeek, faker => faker.PickRandom(1, 5))
                .RuleFor(workoutPlan => workoutPlan.Name, faker => faker.Lorem.Word())
                .Generate());

            return fakeWorkoutPlans;
        }

        public static IEnumerable<Routine> GetPreConfiguredRoutines(List<WorkoutPlan> workoutPlans)
        {
            var rnd = new Random();

            var fakeRoutines = Enumerable
                .Range(1, rnd.Next(10, 15))
                .Select(_ => new Faker<Routine>()
                .RuleFor(routine => routine.WorkoutPlan, workoutPlans[rnd.Next(1, workoutPlans.Count - 1)])
                .RuleFor(routine => routine.DayOrderNumber, faker => faker.PickRandom(1, 6))
                .RuleFor(routine => routine.Name, faker => faker.Lorem.Word()).Generate());


            return fakeRoutines;
        }


        public static IEnumerable<WorkoutSet> GetPreConfiguredWorkoutSets(List<Routine> routines)
        {
            var rnd = new Random();

            var fakeWorkoutSets = Enumerable
                .Range(1, rnd.Next(30, 35))
                .Select(_ => new Faker<WorkoutSet>()
                .RuleFor(workoutSet => workoutSet.Routine, routines[rnd.Next(1, routines.Count - 1)])
                .Generate()
                );



            return fakeWorkoutSets;

        }

        public static IEnumerable<Set> GetPreConfiguredSets(List<WorkoutSet> workoutSets, List<Exercise> exercises)
        {
            var rnd = new Random();

            var fakeSets = Enumerable.Range(1, rnd.Next(50, 60))
                .Select(_ => new Faker<Set>()
                .RuleFor(set => set.WorkoutSet, faker => faker.PickRandom(workoutSets))
                .RuleFor(set => set.Exercise, faker => faker.PickRandom(exercises))
                .RuleFor(set => set.NumberOfReps, rnd.Next(8, 20))
                .RuleFor(set => set.Weight, rnd.Next(50, 120))
                .Generate());

            return fakeSets;

        }

        public static IEnumerable<CompletedRoutine> GetPreConfiguredCompletedRoutines(List<User> users, List<Routine> routines)
        {
            var rnd = new Random();


            var fakeCompletedRoutines = Enumerable.Range(1, 10)
                .Select(_ => new Faker<CompletedRoutine>()
                .RuleFor(completedRoutine => completedRoutine.User, faker => faker.PickRandom(users))
                .RuleFor(completedRoutine => completedRoutine.Name, faker => faker.Lorem.Word())
                .RuleFor(completedRoutine => completedRoutine.Routine, faker => faker.PickRandom(routines))
                .RuleFor(completedRoutine => completedRoutine.CreatedAt, faker => faker.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now))
                .Generate()
                );

            return fakeCompletedRoutines;
        }

        

    }
}
