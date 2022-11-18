using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Domain.Abstractions
{
    public interface IExercisesRepository
    {
        Task<Exercise> AddExercise(Exercise exercise);
        Task<List<Exercise>> AddExercises(List<Exercise> exercises);
        Task<Exercise> GetExerciseById(int id);
        Task<List<Exercise>> GetExercisesByName(string name, PaginationFilter paginationFilter);
        Task<List<Exercise>> GetExercisesByCategory(string category, PaginationFilter paginationFilter);
        Task<PagedList<Exercise>> GetAllExercises(PaginationFilter paginationFilter);
        Task<List<Exercise>> GetAllExercisesWithNoPagination();
        Task<List<string>> GetExercisesCategories();
        Task<IList<ExerciseNameResponse>> GetExercisesNames();
        Task UpdateExercise(Exercise exercise);
        void DeleteExercise(Exercise exercise);
    }
}