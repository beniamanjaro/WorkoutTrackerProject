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
    public class WorkoutPlansRepository : IWorkoutPlansRepository
    {
        WorkoutContext _workoutContext;

        public WorkoutPlansRepository(WorkoutContext context)
        {
            _workoutContext = context;
        }

        public async Task<WorkoutPlan> AddWorkoutPlan(WorkoutPlan workoutPlan)
        {
            var res = await _workoutContext.WorkoutPlans.AddAsync(workoutPlan);
            return res.Entity;
        }

        public async Task<List<WorkoutPlan>> GetAllWorkoutPlans()
        {
            var workoutPlans = await _workoutContext.WorkoutPlans.Include(w => w.Routines)
                .ThenInclude(r => r.WorkoutSets)
                .ThenInclude(ws => ws.Sets).ThenInclude(s => s.Exercise)
                .ToListAsync();

            return workoutPlans;
        }

        public async Task<WorkoutPlan> GetWorkoutPlanById(int id)
        {
            var workoutPlan = await _workoutContext.WorkoutPlans.Include(w => w.Routines)
                .ThenInclude(r => r.WorkoutSets)
                .ThenInclude(ws => ws.Sets).ThenInclude(s => s.Exercise)
                .SingleOrDefaultAsync(w => w.Id == id);

            return workoutPlan;
        }

        public async Task<List<WorkoutPlan>> GetWorkoutPlansByUser(int userId)
        {
            var workoutPlans = await _workoutContext.WorkoutPlans.Where(w => w.UserId == userId)
                .Include(w => w.Routines).ThenInclude(r => r.WorkoutSets)
                .ThenInclude(ws => ws.Sets).ThenInclude(s => s.Exercise)
                .ToListAsync();

            return workoutPlans;

        }
        public async void DeleteWorkoutPlan(WorkoutPlan workoutPlan)
        {
            _workoutContext.WorkoutPlans.Remove(workoutPlan);
        }

        public async Task UpdateWorkoutPlan(WorkoutPlan workoutPlan)
        {
            _workoutContext.WorkoutPlans.Update(workoutPlan);
        }
    }
}
