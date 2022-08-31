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
    public class CompletedRoutinesRepository : ICompletedRoutinesRepository
    {
        private readonly WorkoutContext _workoutContext;

        public CompletedRoutinesRepository(WorkoutContext context)
        {
            _workoutContext = context;
        }
        public async Task<CompletedRoutine> GetCompletedRoutineById(int id)
        {
            var completedRoutine = await _workoutContext.CompletedRoutines.SingleOrDefaultAsync(cr => cr.CompletedRoutineId == id);
            return completedRoutine;
        }

        public async Task<List<CompletedRoutine>> GetWorkoutPlansByUser(int userId)
        {
            var completedRoutines = await _workoutContext.CompletedRoutines.Where(cr => cr.UserId == userId)
                .Include(cr => cr.WorkoutSets).ThenInclude(ws => ws.Sets)
                .ThenInclude(s => s.Exercise).ToListAsync();
            return completedRoutines;
        }
        public async Task<CompletedRoutine> AddCompletedRoutine(CompletedRoutine completedRoutine)
        {
            var completedRoutineToAdd = await _workoutContext.CompletedRoutines.AddAsync(completedRoutine);
            return completedRoutineToAdd.Entity;
        }

        public void DeleteCompletedRoutine(CompletedRoutine completedRoutine)
        {
            _workoutContext.CompletedRoutines.Remove(completedRoutine);
        }


        public async Task UpdateCompletedRoutine(CompletedRoutine completedRoutine)
        {
            _workoutContext.CompletedRoutines.Update(completedRoutine);
        }

    }
}
