namespace WorkoutTracker.Presentation.DTOs
{
    public class SetGetDto
    {
        public int Id { get; set; }
        public ExerciseGetDto Exercise { get; set; }
        public int NumberOfReps { get; set; }
        public int Weight { get; set; }
    }
}
