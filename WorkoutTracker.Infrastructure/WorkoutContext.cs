using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WorkoutTracker.Infrastructure
{
    public class WorkoutContext : IdentityDbContext
    {

        public WorkoutContext() : base() { }

    public WorkoutContext (DbContextOptions<WorkoutContext> optionsBuilder) : base(optionsBuilder) 
        {

        }


        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<CompletedRoutine> CompletedRoutines { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
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
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Exercise>().HasData(
                
            //    new Exercise() { Id = 1, Name = "Bench Press", Category = "Chest", Equipment = "Bench, Barbell" },
            //    new Exercise() { Id = 2, Name = "Cable Crossover", Category = "Chest", Equipment = "Cable Machine" },
            //    new Exercise() { Id = 3, Name = "Dumbbell Flies", Category = "Chest", Equipment = "Bench, Dumbbells" },
            //    new Exercise() { Id = 4, Name = "Dumbbell Press", Category = "Chest", Equipment = "Bench, Dumbbells" },
            //    new Exercise() { Id = 5, Name = "Incline Bench Press", Category = "Chest", Equipment = "Bench, Barbell" },
            //    new Exercise() { Id = 6, Name = "Incline Dumbbell Press", Category = "Chest", Equipment = "Bench, Dumbbells" },
            //    new Exercise() { Id = 7, Name = "Squat", Category = "Legs", Equipment = "Barbell" },
            //    new Exercise() { Id = 8, Name = "Calf Raises", Category = "Legs", Equipment = "" },
            //    new Exercise() { Id = 9, Name = "Front Squat", Category = "Legs", Equipment = "Barbell" },
            //    new Exercise() { Id = 10, Name = "Leg Curls", Category = "Legs", Equipment = "Leg Curls Machine" },
            //    new Exercise() { Id = 11, Name = "Leg Extensions", Category = "Legs", Equipment = "Leg Extension Machine" },
            //    new Exercise() { Id = 12, Name = "Leg Press", Category = "Legs", Equipment = "Leg Press Machine" },
            //    new Exercise() { Id = 13, Name = "Lunges", Category = "Legs", Equipment = "Barbell" },
            //    new Exercise() { Id = 14, Name = "Seated Calf Raises", Category = "Legs", Equipment = "" },
            //    new Exercise() { Id = 15, Name = "Dumbbell Lateral Raises", Category = "Shoulders", Equipment = "Dumbbell" },
            //    new Exercise() { Id = 16, Name = "Military Press", Category = "Shoulders", Equipment = "Barbell" },
            //    new Exercise() { Id = 17, Name = "Shoulder Dumbbell Press", Category = "Shoulders", Equipment = "Dumbbell" },
            //    new Exercise() { Id = 18, Name = "Upright Rows", Category = "Shoulders", Equipment = "Barbell" },
            //    new Exercise() { Id = 19, Name = "Assited Dips", Category = "Triceps", Equipment = "" },
            //    new Exercise() { Id = 20, Name = "Close Grip Bench Press", Category = "Triceps", Equipment = "Barbell Bench" },
            //    new Exercise() { Id = 21, Name = "Dips", Category = "Triceps", Equipment = "" },
            //    new Exercise() { Id = 22, Name = "Pushdowns", Category = "Triceps", Equipment = "Cable" },
            //    new Exercise() { Id = 23, Name = "Assisted Chin Up", Category = "Back", Equipment = "" },
            //    new Exercise() { Id = 24, Name = "Assisted Pull Up", Category = "Back", Equipment = "" },
            //    new Exercise() { Id = 25, Name = "Pull up", Category = "Back", Equipment = "" },
            //    new Exercise() { Id = 26, Name = "Chin Up", Category = "Back", Equipment = "" },
            //    new Exercise() { Id = 27, Name = "Barbell Row", Category = "Back", Equipment = "Barbell" },
            //    new Exercise() { Id = 28, Name = "Cable Row", Category = "Back", Equipment = "Cable" },
            //    new Exercise() { Id = 29, Name = "Deadlift", Category = "Back", Equipment = "Barbell" },
            //    new Exercise() { Id = 30, Name = "Dumbbell Row", Category = "Back", Equipment = "Dumbbell" },
            //    new Exercise() { Id = 31, Name = "Hyperextensions", Category = "Back", Equipment = "" },
            //    new Exercise() { Id = 32, Name = "Pulldowns", Category = "Back", Equipment = "Cable" }
                
            //    );
            modelBuilder.Entity<CompletedRoutine>()
            .HasOne<User>()
            .WithMany(u => u.CompletedRoutines)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
            .HasMany(u => u.CompletedRoutines)
            .WithOne(cr => cr.User)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
            .HasMany(u => u.CompletedRoutines)
            .WithOne(cr => cr.User)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutPlan>()
            .HasMany(u => u.Users)
            .WithMany(wp => wp.WorkoutPlans)
            .UsingEntity<Dictionary<string, object>>(
                "UserWorkoutPlan",
                    j => j.HasOne<User>().WithMany().OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<WorkoutPlan>().WithMany().OnDelete(DeleteBehavior.Cascade));


        }


    }
}
