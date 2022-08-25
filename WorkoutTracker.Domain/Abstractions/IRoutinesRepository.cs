using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Domain.Abstractions
{
    public interface IRoutinesRepository
    {
        Task<Routine> CreateRoutine(Routine routine);
        void DeleteRoutineById(int id);
        Task<List<Routine>> GetAllRoutines(int id);
        Task<Routine> GetRoutineById(int id);
        void UpdateRoutineById(int id, Routine routine);
    }
}