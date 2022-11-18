using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class CompletedRoutine
    {
        public int CompletedRoutineId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<CompletedRoutineExercise> Exercises { get; set; }
        public string WorkoutPlanName { get; set; }
        public int? WorkoutPlanId { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; }
        public string RoutineName { get; set; }
        public int TotalVolume { get; set; }
        public int TotalReps{ get; set; }
        public int TotalSets { get; set; }
    }
}
