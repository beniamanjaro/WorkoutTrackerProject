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
    public class WorkoutSetsRepository : IWorkoutSetsRepository
    {
        WorkoutContext _workoutContext;

        public WorkoutSetsRepository(WorkoutContext context)
        {
            _workoutContext = context;
        }


        public async Task<WorkoutSet> AddWorkoutSet(WorkoutSet workoutSet)
        {
            var res = await _workoutContext.WorkoutSets.AddAsync(workoutSet);
            return res.Entity;
        }

        public async Task<List<WorkoutSet>> GetAllWorkoutSets()
        {
            return await _workoutContext.WorkoutSets.ToListAsync();
        }

        public async Task<WorkoutSet> GetWorkoutSetById(int id)
        {
            return await _workoutContext.WorkoutSets.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task UpdateWorkoutSet(WorkoutSet workoutSet)
        {
            _workoutContext.WorkoutSets.Update(workoutSet);
        }

        public void DeleteWorkoutSet(WorkoutSet workoutSet)
        {
            _workoutContext.WorkoutSets.Remove(workoutSet);
        }
    }
}
