using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class RoutineGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DayOrderNumber { get; set; }
        public ICollection<WorkoutSetGetDto> WorkoutSets { get; set; }
    }
}
