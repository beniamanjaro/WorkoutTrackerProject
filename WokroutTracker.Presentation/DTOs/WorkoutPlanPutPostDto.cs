using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class WorkoutPlanPutPostDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int TimesPerWeek { get; set; }
        public ICollection<RoutinePostDto> Routines { get; set; }
    }
}
