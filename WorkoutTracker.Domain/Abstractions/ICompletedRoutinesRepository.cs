using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Domain.Abstractions
{
    public interface ICompletedRoutinesRepository
    {
        Task<CompletedRoutine> AddCompletedRoutine(CompletedRoutine completedRoutine);
        Task<CompletedRoutine> GetCompletedRoutineById(int id);
        Task<List<CompletedRoutine>> GetCompletedRoutinesByUser(int userId);
        Task<List<CompletedRoutine>> GetCompletedRoutinesByWorkoutPlan(int workoutPlanId);
        Task<PagedList<CompletedRoutine>> GetCompletedRoutinesByUserByTimeframe(int userId, int timeframeInMonths, PaginationFilter paginationFilter);
        Task<PagedList<CompletedRoutine>> GetCompletedRoutinesByWorkoutPlanByUser(int userId, int workoutPlanId, PaginationFilter paginationFilter);
        Task<CompletedRoutine> GetMostRecentCompletedRoutinesExercisesStatsByUserByWorkoutPlanByName(int userId, int workoutPlanId, string routineName);
        Task UpdateCompletedRoutine(CompletedRoutine completedRoutine);
        void DeleteCompletedRoutine(CompletedRoutine completedRoutine);

    }
}
