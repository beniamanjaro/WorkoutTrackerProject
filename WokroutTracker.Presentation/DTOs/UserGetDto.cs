using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class UserGetDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int? SelectedWorkoutPlanId { get; set; }
        public ICollection<WorkoutPlanGetDto> WorkoutPlans { get; set; }
        public ICollection<CompletedRoutineGetDto> CompletedRoutines { get; set; }
    }
}
