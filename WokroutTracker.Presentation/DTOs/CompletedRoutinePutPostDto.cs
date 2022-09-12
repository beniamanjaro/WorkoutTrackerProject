using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class CompletedRoutinePutPostDto
    {
        public string Name { get; set; }
        public int RoutineId { get; set; }
    }
}
