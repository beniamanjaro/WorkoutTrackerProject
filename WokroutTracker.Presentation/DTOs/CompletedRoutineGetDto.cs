using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class CompletedRoutineGetDto
    {
        public string WorkoutPlanName { get; set; }
        public int WorkoutPlanId { get; set; }
        public string RoutineName { get; set; }
        public int CompletedRoutineId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public ICollection<CompletedRoutineExerciseGetDto> Exercises { get; set; }
        public int TotalVolume { get; set; }
        public int TotalReps { get; set; }
        public int TotalSets { get; set; }
    }
}
