using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class WorkoutPlanGetDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public int TimesPerWeek { get; set; }
        public ICollection<UserGetIdDto> Users { get; set; }
        public ICollection<RoutineGetDto> Routines { get; set; }

    }
}
