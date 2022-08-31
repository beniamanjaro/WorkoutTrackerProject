using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Infrastructure
{
    public class WorkoutContext : DbContext
    {

        public WorkoutContext()
        {

        }

        public WorkoutContext (DbContextOptions<WorkoutContext> optionsBuilder) : base(optionsBuilder) 
        {

        }


        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<CompletedRoutine> CompletedRoutines { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<PrimaryMuscle> PrimaryMuscles { get; set; }
        public DbSet<WorkoutSet> WorkoutSets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Set> Sets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=WorkoutsSampleDb;"
);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise() { Id = 1, Name = "Bench Press", Category = "Chest", Equipment = "Bench, Barbell" },
                new Exercise() { Id = 2, Name = "Cable Crossover", Category = "Chest", Equipment = "Cable Machine" },
                new Exercise() { Id = 3, Name = "Dumbbell Flies", Category = "Chest", Equipment = "Bench, Dumbbells" },
                new Exercise() { Id = 4, Name = "Dumbbell Press", Category = "Chest", Equipment = "Bench, Dumbbells" },
                new Exercise() { Id = 5, Name = "Incline Benchpress", Category = "Chest", Equipment = "Bench, Barbell" },
                new Exercise() { Id = 6, Name = "Incline Dumbbell Press", Category = "Chest", Equipment = "Bench, Dumbbells" },
                new Exercise() { Id = 8, Name = "Squat", Category = "Legs", Equipment = "Barbell" },
                new Exercise() { Id = 9, Name = "Calf Raises", Category = "Legs", Equipment = "" },
                new Exercise() { Id = 10, Name = "Front Squat", Category = "Legs", Equipment = "Barbell" },
                new Exercise() { Id = 11, Name = "Leg Curls", Category = "Legs", Equipment = "Leg Curls Machine" },
                new Exercise() { Id = 12, Name = "Leg Extensions", Category = "Legs", Equipment = "Leg Extension Machine" },
                new Exercise() { Id = 13, Name = "Leg Press", Category = "Legs", Equipment = "Leg Press Machine" }
                );

            modelBuilder.Entity<PrimaryMuscle>().HasData(
                new PrimaryMuscle() { Id = 1, ExerciseId = 1, Name = "Chest" }
                );

            modelBuilder.Entity<User>().HasData(
                new User () { Id = 1, Email = "email@gmail.com", Password = "test123", Username = "test user" }
                  );

            modelBuilder.Entity<WorkoutPlan>().HasData(
                new WorkoutPlan() { Id = 1, Name = "Push Pull Legs", TimesPerWeek = 6, UserId = 1  }
                 );

            modelBuilder.Entity<Routine>().HasData(
                new Routine() { Id = 1, WorkoutPlanId = 1, DayOrderNumber = 1, Name = "Chest day" }
                 );

            modelBuilder.Entity<WorkoutSet>().HasData(
                new WorkoutSet() { Id = 1, RoutineId = 1 },
                new WorkoutSet() { Id = 2, RoutineId = 1 }

                 );

            modelBuilder.Entity<Set>().HasData(
                new Set() { Id = 1, ExerciseId = 1, WorkoutSetId = 1, NumberOfReps = 8, Weight = 80 },
                new Set() { Id = 2, ExerciseId = 1, WorkoutSetId = 1, NumberOfReps = 8, Weight = 70 },
                new Set() { Id = 3, ExerciseId = 1, WorkoutSetId = 1, NumberOfReps = 8, Weight = 60 },
                new Set() { Id = 4, ExerciseId = 2, WorkoutSetId = 2, NumberOfReps = 5, Weight = 55 },
                new Set() { Id = 5, ExerciseId = 2, WorkoutSetId = 2, NumberOfReps = 5, Weight = 45 }
                 );






        }


    }
}
