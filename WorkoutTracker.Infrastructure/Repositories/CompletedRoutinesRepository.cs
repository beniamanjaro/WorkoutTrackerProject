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
            var completedRoutine = await _workoutContext.CompletedRoutines
                .Include(cr => cr.Routine)
                .ThenInclude(cr => cr.WorkoutSets).ThenInclude(ws => ws.Sets)
                .ThenInclude(s => s.Exercise).SingleOrDefaultAsync(cr => cr.CompletedRoutineId == id);
            return completedRoutine;
        }

        public async Task<List<CompletedRoutine>> GetCompletedRoutinesByUser(int userId)
        {
            var completedRoutines = await _workoutContext.CompletedRoutines.Where(cr => cr.UserId == userId)
                .Include(cr => cr.Routine)
                .ThenInclude(cr => cr.WorkoutSets).ThenInclude(ws => ws.Sets)
                .ThenInclude(s => s.Exercise).ToListAsync();
            return completedRoutines;
        }
        public async Task<CompletedRoutine> AddCompletedRoutine(CompletedRoutine completedRoutine)
        {
            var completedRoutineToAdd = await _workoutContext.CompletedRoutines.AddAsync(completedRoutine);
            return completedRoutineToAdd.Entity;
        }

        public async Task<List<CompletedRoutine>> GetCompletedRoutinesByUserByTimeframe(int userId, int timeframeInMonths)
        {
            var completedRoutines = await _workoutContext.CompletedRoutines
                .Where(cr => cr.UserId == userId)
                .Where(cr => cr.CreatedAt > DateTime.Now.AddMonths(timeframeInMonths * -1))
                .OrderByDescending(cr => cr.CreatedAt)
                .ToListAsync();

            return completedRoutines;
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
