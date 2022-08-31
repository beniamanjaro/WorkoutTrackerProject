using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Domain.Abstractions
{
    public interface IRoutinesRepository
    {
        Task<Routine> AddRoutine(Routine routine);
        void DeleteRoutine(Routine routine);
        Task<List<Routine>> GetAllRoutines();
        Task<Routine> GetRoutineById(int id);
        void UpdateRoutineById(int id, Routine routine);
    }
}