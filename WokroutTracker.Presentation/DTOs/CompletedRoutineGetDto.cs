using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class CompletedRoutineGetDto
    {
        public int CompletedRoutineId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<WorkoutSetGetDto> WorkoutSets { get; set; }
    }
}
