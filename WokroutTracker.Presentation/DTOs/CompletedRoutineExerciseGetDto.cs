namespace WorkoutTracker.Presentation.DTOs
{
    public class CompletedRoutineExerciseGetDto
    {
        public int Id { get; set; }
        public ExerciseGetDto Exercise { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }
    }
}
