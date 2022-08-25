using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Infrastructure.Repositories
{
    public class RoutinesRepository : IRoutinesRepository
    {

        WorkoutContext _workoutContext;

        public RoutinesRepository( WorkoutContext context)
        {
            _workoutContext = context;
        }

        public async Task<Routine> CreateRoutine(Routine routine)
        {
            var newRoutine = await _workoutContext.Routines.AddAsync(routine);
            return newRoutine.Entity;
        }
        public async Task<Routine> GetRoutineById(int id)
        {
            return await _workoutContext.Routines.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Routine>> GetAllRoutines(int id)
        {
            return await _workoutContext.Routines.Include(r => r.WorkoutSets).DefaultIfEmpty().ToListAsync();
        }

        public void DeleteRoutineById(int id)
        {
            var routineToDelete = _workoutContext.Routines.First(r => r.Id == id);
            _workoutContext.Routines.Remove(routineToDelete);
        }

        public void UpdateRoutineById(int id, Routine routine)
        {
            var routineToUpdate = _workoutContext.Routines.First(e => e.Id == id);
            routineToUpdate.Name = routine.Name;
            routineToUpdate.WorkoutSets = routine.WorkoutSets;
            routineToUpdate.DayOrderNumber = routine.DayOrderNumber;
            routineToUpdate.WorkoutPlanId = routine.WorkoutPlanId;
        }


    }
}
