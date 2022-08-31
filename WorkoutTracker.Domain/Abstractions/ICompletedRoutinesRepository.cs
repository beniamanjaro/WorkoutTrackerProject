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
        Task<List<CompletedRoutine>> GetWorkoutPlansByUser(int userId);
        Task UpdateCompletedRoutine(CompletedRoutine completedRoutine);
        void DeleteCompletedRoutine(CompletedRoutine completedRoutine);

    }
}
