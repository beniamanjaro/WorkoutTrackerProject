using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Domain.Abstractions
{
    public interface IWorkoutSetsRepository
    {
        public Task<WorkoutSet> AddWorkoutSet(WorkoutSet workoutPlan);
        public Task<WorkoutSet> GetWorkoutSetById(int id);
        public Task<List<WorkoutSet>> GetAllWorkoutSets();
        public Task UpdateWorkoutSet(WorkoutSet workoutSet);
        public void DeleteWorkoutSet(WorkoutSet workoutSet);

    }
}
