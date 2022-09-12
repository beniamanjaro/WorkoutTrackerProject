using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;

namespace WorkoutTracker.Infrastructure.Repositories
{
    public class ExercisesRepository : IExercisesRepository
    {
        WorkoutContext _workoutContext;

        public ExercisesRepository(WorkoutContext context)
        {
            _workoutContext = context;
        }


        public async Task<Exercise> AddExercise(Exercise exercise)
        {
            var newExercise = await _workoutContext.Exercises.AddAsync(exercise);
            return newExercise.Entity;
        }
        public async Task<Exercise> GetExerciseById(int id)
        {
            return await _workoutContext.Exercises.Include(e => e.PrimaryMuscles)
                .Include(e => e.SecondaryMuscles).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Exercise> GetExerciseByName(string name)
        {
            return await _workoutContext.Exercises
                .DefaultIfEmpty()
                .FirstOrDefaultAsync(e => e.Name == name);
        }

        public async Task<List<Exercise>> GetExercisesByCategory(string category)
        {
            return await _workoutContext.Exercises
                .Where(e => e.Category.ToLower() == category.ToLower())
                .Include(e => e.PrimaryMuscles)
                .Include(e => e.SecondaryMuscles)
                .ToListAsync();
        }
        public async Task<List<Exercise>> GetAllExercises()
        {
            return await _workoutContext.Exercises.Include(e => e.PrimaryMuscles)
                .Include(e => e.SecondaryMuscles).ToListAsync();
        }


        public void DeleteExercise(Exercise exercise)
        {
            _workoutContext.Exercises.Remove(exercise);
        }

        public async Task UpdateExercise(Exercise exercise)
        {
            _workoutContext.Exercises.Update(exercise);
        }

        
    }
}
