using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.DTOs
{
    public class CompletedRoutinePutPostDto
    {
        public string WorkoutPlanName { get; set; }
        public int? WorkoutPlanId { get; set; }
        public string RoutineName { get; set; }
        public int UserId { get; set; }
        public ICollection<CompletedRoutineExercisePostDto> Exercises { get; set; }
        public int TotalVolume { get; set; }
        public int TotalReps { get; set; }
        public int TotalSets { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}
