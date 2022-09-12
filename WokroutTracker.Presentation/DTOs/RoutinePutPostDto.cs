namespace WorkoutTracker.Presentation.DTOs
{
    public class RoutinePutPostDto
    {
        public string Name { get; set; }
        public int DayOrderNumber { get; set; }
        public ICollection<WorkoutSetPutPostDto> WorkoutSets { get; set; }
    }
}
