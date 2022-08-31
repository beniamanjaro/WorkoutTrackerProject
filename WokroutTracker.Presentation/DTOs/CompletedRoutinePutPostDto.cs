using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class CompletedRoutinePutPostDto
    {
        public string Name { get; set; }
        public ICollection<WorkoutSetPostDto> WorkoutSets { get; set; }
    }
}
