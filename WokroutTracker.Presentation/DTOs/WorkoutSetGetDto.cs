using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class WorkoutSetGetDto
    {
        public int Id { get; set; }
        public ICollection<SetGetDto> Sets { get; set; }
    }
}
