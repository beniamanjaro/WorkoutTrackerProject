using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class WorkoutPlanPostDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int TimesPerWeek { get; set; }
        public ICollection<RoutinePutPostDto> Routines { get; set; }
    }
}
