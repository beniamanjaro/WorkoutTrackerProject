using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class CompletedRoutineExercisePostDto
    {
        public int ExerciseId { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }
    }
}
