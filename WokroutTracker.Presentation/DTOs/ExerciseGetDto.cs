using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class ExerciseGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Equipment { get; set; }
        public string GifLink { get; set; }
        public string Muscle { get; set; }
    }
}
