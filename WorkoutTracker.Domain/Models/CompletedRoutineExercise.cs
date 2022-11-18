using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class CompletedRoutineExercise
    {
        public int Id { get; set; }
        public int CompletedRoutineId { get; set; }
        public CompletedRoutine CompletedRoutine { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }

    }
}
