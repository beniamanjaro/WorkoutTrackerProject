using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Domain.Abstractions
{
    public interface IWorkoutPlansRepository
    {
        Task<WorkoutPlan> AddWorkoutPlan(WorkoutPlan workoutPlan);
        Task<WorkoutPlan> GetWorkoutPlanById(int id);
        Task<List<WorkoutPlan>> GetWorkoutPlansByUser(int userId);
        Task<List<WorkoutPlan>> GetWorkoutPlansSubscriptionsByUser(int userId);
        Task<List<WorkoutPlan>> GetPopularWorkoutPlans(PaginationFilter paginationFilter);
        Task<List<WorkoutPlan>> GetAllWorkoutPlansBySearchValue(string searchValue);
        Task<List<WorkoutPlan>> GetAllWorkoutPlans();
        Task UpdateWorkoutPlan(WorkoutPlan workoutPlan);
        void DeleteWorkoutPlan(WorkoutPlan workoutPlan);


    }
}
