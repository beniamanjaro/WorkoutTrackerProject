using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Infrastructure
{
    public class test
    {
        static public async Task<IEnumerable<Exercise>> Testogre()
        {
            await using var dbcontext = new WorkoutContext();
            

            var exercises = await dbcontext.Exercises.Include(e => e.PrimaryMuscles).ToListAsync();
            return exercises;

        }

        static public async Task<Exercise> AddExercise(Exercise exercise)
        {

            await using var dbcontext = new WorkoutContext();

            var newExercise = await dbcontext.Exercises.AddAsync(exercise);

            return newExercise.Entity;
        }

        static public async Task<List<Exercise>> GetExercisesByCategory(string category)
        {
            await using var dbcontext = new WorkoutContext();

            return await dbcontext.Exercises.Where(e => e.Category.ToLower() == category.ToLower()).ToListAsync();
        }



        static public async Task<Routine> GetRoutineById(int id)
        {
            await using var _workoutContext = new WorkoutContext();

            _workoutContext.Database.EnsureDeleted();
            _workoutContext.Database.EnsureCreated();

            return await _workoutContext.Routines.Include(r => r.WorkoutSets).ThenInclude(w => w.Sets).ThenInclude(s => s.Exercise).FirstOrDefaultAsync(e => e.Id == id);
        }


    }
}
