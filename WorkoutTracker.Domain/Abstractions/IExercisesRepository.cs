namespace WorkoutTracker.Domain.Abstractions
{
    public interface IExercisesRepository
    {
        Task<Exercise> AddExercise(Exercise exercise);
        Task<Exercise> GetExerciseById(int id);
        Task<Exercise> GetExerciseByName(string name);
        Task<List<Exercise>> GetExercisesByCategory(string category);
        Task<List<Exercise>> GetAllExercises();
        Task UpdateExercise(Exercise exercise);
        void DeleteExercise(Exercise exercise);
    }
}