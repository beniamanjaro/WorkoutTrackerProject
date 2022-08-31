namespace WorkoutTracker.Presentation.DTOs
{
    public class RoutinePostDto
    {
        public string Name { get; set; }
        public int DayOrderNumber { get; set; }
        public ICollection<WorkoutSetPostDto> WorkoutSets { get; set; }
    }
}
